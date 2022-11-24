using System;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;
using UnityEngine.Events;
      
    public class Interstitial : MonoBehaviour
    {
        public GameObject btnPlayAgain;
        public GameObject iconLoading;
        public UnityEvent OnAdsDone = new UnityEvent();

       [Header("Ad Unit Ids"), Tooltip("Android Ad Unit Ids")]
        public string androidAdUnitId;

        [Tooltip("iOS Ad Unit Ids")]
        public string iosAdUnitId;
        IInterstitialAd m_InterstitialAd;

        async void Start()
        {
            iconLoading.SetActive(true);
            try
            {
                await UnityServices.InitializeAsync();

                InitializationComplete();
            }
            catch (Exception e)
            {
                InitializationFailed(e);
                OnAdsDone?.Invoke();
            }
        }

        void OnDestroy()
        {
            m_InterstitialAd?.Dispose();
        }

        public async void ShowInterstitial()
        {
            if (m_InterstitialAd?.AdState == AdState.Loaded)
            {
                try
                {
                    var showOptions = new InterstitialAdShowOptions { AutoReload = true };
                    await m_InterstitialAd.ShowAsync(showOptions);
                }
                catch (ShowFailedException e)
                {
                    OnAdsDone?.Invoke();
                }
            }
        }

        async void LoadAd()
        {
            iconLoading.SetActive(true);
            btnPlayAgain.SetActive(false);
            try
            {
                await m_InterstitialAd.LoadAsync();
            }
            catch (LoadFailedException)
            {

            }
        }

        void InitializationComplete()
        {
            // Impression Event
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;

            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    m_InterstitialAd = MediationService.Instance.CreateInterstitialAd(androidAdUnitId);
                    break;

                case RuntimePlatform.IPhonePlayer:
                    m_InterstitialAd = MediationService.Instance.CreateInterstitialAd(iosAdUnitId);
                    break;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.LinuxEditor:
                    m_InterstitialAd = MediationService.Instance.CreateInterstitialAd(!string.IsNullOrEmpty(androidAdUnitId) ? androidAdUnitId : iosAdUnitId);
                    break;
                default:
                    Debug.LogWarning("Mediation service is not available for this platform:" + Application.platform);
                    return;
            }

            // Load Events
            m_InterstitialAd.OnLoaded += AdLoaded;
            m_InterstitialAd.OnFailedLoad += AdFailedLoad;

            // Show Events
            m_InterstitialAd.OnClosed += AdClosed;

            Debug.Log("Initialized On Start! Loading Ad...");
            LoadAd();
        }

        void InitializationFailed(Exception error)
        {
            SdkInitializationError initializationError = SdkInitializationError.Unknown;
            if (error is InitializeFailedException initializeFailedException)
            {
                initializationError = initializeFailedException.initializationError;
            }
            OnAdsDone?.Invoke();
        }

        void AdClosed(object sender, EventArgs args)
        {
            Debug.Log("Interstitial Closed! Loading Ad...");
            OnAdsDone?.Invoke();
        }

        void AdLoaded(object sender, EventArgs e)
        {
            btnPlayAgain.SetActive(true);
            iconLoading.SetActive(false);
        }

        void AdFailedLoad(object sender, LoadErrorEventArgs e)
        {
            iconLoading.SetActive(false);
            btnPlayAgain.SetActive(true);
        }

        void ImpressionEvent(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
            Debug.Log($"Impression event from ad unit id {args.AdUnitId} : {impressionData}");
        }
    }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public bool isMute;
    private SettingData settingData;
    private Button button;
    private AudioManager soundManager;

    private void Awake() {
        soundManager = AudioManager.Instance;
        button = GetComponent<Button>();
    }

    private void OnEnable() {
        soundManager.OnMute.AddListener(OnMute);

        button.onClick.AddListener(OnClick);
    }
    // Start is called before the first frame update
    void Start()
    {
        settingData = SettingData.Load();
        OnMute(settingData.mute);
    }
    
    private void OnMute(bool mute) {
        if(mute) {
            gameObject.SetActive(!isMute);
        } else {
            gameObject.SetActive(isMute);
        }
    }

    private void OnClick() {
        soundManager.MuteGame(isMute);
    }

    private void OnDestroy() {
        soundManager.OnMute.RemoveListener(OnMute);
    }
}

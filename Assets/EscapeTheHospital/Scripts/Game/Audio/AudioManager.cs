using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.Events;

public class AudioManager : Singleton<AudioManager>
{

    private AudioSource audioSource;
    private SettingData settingData;
    public UnityEvent<bool> OnMute;

    protected override void Awake()
    {
        base.Awake();
        settingData = SettingData.Load();
        audioSource = GetComponent<AudioSource>();
        Debug.Log(audioSource);
    }

    private void Start() {
        audioSource.mute = settingData.mute;
    }

    public void PlayOneShot(AudioClip audioClip, float volumeScale = 1) {
        audioSource.PlayOneShot(audioClip, volumeScale);
    }

    public void MuteGame(bool mute) {
        settingData.mute = mute;
        settingData.Save();
        audioSource.mute = mute;
        OnMute?.Invoke(mute);
    }


}

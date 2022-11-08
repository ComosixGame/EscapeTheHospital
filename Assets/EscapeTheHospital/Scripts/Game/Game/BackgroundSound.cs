using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip audioWin;
    // public AudioClip audioLose;
    private AudioManager soundManager;
    private GameManager gameManager;
    private AudioSource audioSource;

    private void Awake() {
        soundManager = AudioManager.Instance;
        gameManager = GameManager.Instance;
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = SettingData.Load().mute;
    }

    private void OnEnable() {
        soundManager.OnMute.AddListener(OnMuteGame);
        gameManager.onEndGame.AddListener(OnEndGame);
    }

    private void OnMuteGame(bool mute) {
        audioSource.mute = mute;
    }

    private void OnEndGame(bool isWin) {
        if(isWin) {
            audioSource.clip = audioWin;
        } 
        // else {
        //     audioSource.clip = audioLose;
        // }
        StartCoroutine(PlayAudioEndGame());
    }

    private void OnDisable() {
        soundManager.OnMute.RemoveListener(OnMuteGame);
        gameManager.onEndGame.RemoveListener(OnEndGame);
    }

    IEnumerator PlayAudioEndGame() {
        yield return new WaitForSeconds(0.3f);
        audioSource.Play();
    }
}

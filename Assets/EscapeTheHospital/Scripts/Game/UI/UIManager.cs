using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;
    private AudioManager _audioManager;
    public GameObject header,pauseMenu, loseMenu, winMenu;
    private int _OpenMenuHash;
    private int _CloseMenuHash;
    private Animator _animator;

    public float volumeScale;


    private void Awake() 
    {
        _gameManager = GameManager.Instance;
        _audioManager = AudioManager.Instance;

        _animator = GetComponent<Animator>();

        _OpenMenuHash = Animator.StringToHash("OpenMenu");
        _CloseMenuHash = Animator.StringToHash("CloseMenu");
    }
    
    private void OnEnable() 
    {
        _gameManager.onEndGame.AddListener(OnEndGame);
    }

    public void StartGame()
    {
        _gameManager.StartGame();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        _animator.SetTrigger(_OpenMenuHash);
        _gameManager.PauseGame();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        _gameManager.ResumeGame();
    }

    public void PlaySound(AudioClip audioClip) {
        _audioManager.PlayOneShot(audioClip, volumeScale);
    }


    private void OnEndGame(bool isWin)
    {
        header.SetActive(false);
        if (isWin)
        {
            winMenu.SetActive(true);
        }
        else
        {
            loseMenu.SetActive(true);
        }
    }
}

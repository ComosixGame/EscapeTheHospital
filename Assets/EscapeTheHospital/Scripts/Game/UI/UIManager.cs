using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;
    private AudioManager _audioManager;
    public GameObject header,pauseMenu, loseMenu, winMenu;

    private void Awake() 
    {
        _gameManager = GameManager.Instance;
        _audioManager = AudioManager.Instance;
    }
    
    private void OnEnable() 
    {
        _gameManager.onEndGame.AddListener(OnEndGame);
    }

    private void OnEndGame(bool isWin)
    {
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

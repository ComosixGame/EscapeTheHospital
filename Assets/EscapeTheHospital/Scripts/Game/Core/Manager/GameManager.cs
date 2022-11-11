using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    private PlayerData _playerData;
    private bool _isWin, _isLose;
    public UnityEvent<int> onUpdateCoins = new UnityEvent<int>();
    public UnityEvent onPlayerCatched = new UnityEvent();
    public UnityEvent<Vector3> onPlayerDetected = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> OnDetectedLostkey = new UnityEvent<Vector3>();
    public UnityEvent OnPlayerInvisible = new UnityEvent();

    public UnityEvent onStart = new UnityEvent();
    public UnityEvent onPause = new UnityEvent();
    public UnityEvent onResume = new UnityEvent();
    public UnityEvent<int> onUpdateDoc = new UnityEvent<int>();
    public UnityEvent<bool> onEndGame = new UnityEvent<bool>();
    public UnityEvent<bool> onAdsCompelete = new UnityEvent<bool>();

    protected override void Awake ()
    {
        base.Awake();
        _playerData = PlayerData.LoadData();
    }

    private void OnEnable() 
    {
        
    }

    void Start ()
    {
        Application.targetFrameRate = 60;
        InitGame();
    }


    public void UnlockNewLevel(int indexLevel) {
        List<int> list = _playerData.totalLevels;
        if(list.IndexOf(indexLevel) == -1) {
            _playerData.totalLevels.Add(indexLevel);
            _playerData.SaveData();
        }
    }

    public void InitGame ()
    {
        _isLose = false;
        _playerData = PlayerData.LoadData();
        onUpdateDoc?.Invoke(_playerData.currentDocs);
    }

    public void StartGame()
    {
        onStart?.Invoke();
    }

    public void PauseGame()
    {
        onPause?.Invoke();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        onResume?.Invoke();
        Time.timeScale = 1;
    }

    public void Done()
    {
        onAdsCompelete?.Invoke(true);
    }

  

    public void EndGame(bool win)
    {
        _isWin = win;
        onEndGame?.Invoke(_isWin);
        onUpdateDoc?.Invoke(_playerData.currentDocs);
        _playerData.SaveData();
    }

    public void PlayerDetected(Vector3 playerPos)
    {
        onPlayerDetected?.Invoke(playerPos);
    }

    public void PlayerHaskKey(Vector3 keyPos)
    {
        OnDetectedLostkey?.Invoke(keyPos);
    }

    public void PlayerInvisible()
    {
        OnPlayerInvisible?.Invoke();
    }

    public void UpdateCurrency (int coins)
    {
        _playerData.currentCoins += coins;
        onUpdateCoins?.Invoke(_playerData.currentCoins);
    }

    public void UpdateDocs(int docs)
    {
        if (_isLose) return;
        _playerData.currentDocs += docs;
        onUpdateDoc?.Invoke(_playerData.currentDocs);
    }

    
}

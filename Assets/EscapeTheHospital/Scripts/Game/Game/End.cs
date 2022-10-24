using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private PlayerData _playerData;
    // Start is called before the first frame update
    void Start()
    {
        PlayerData.LoadData();
        GameManager.onEndEvent.AddListener(GameEnd);
    }

    private void GameEnd(int id)
    {
        if (Door.isWin)
        {
            _playerData.totalLevels.Add(id);
            // GameManager.Instance.UpdateCurrency();
        }
        else
        {
            // GameManager.Instance.UpdateCurrency();
        }
        _playerData.SaveData();
    }

    private void OnDisable() 
    {
        GameManager.onEndEvent.RemoveListener(GameEnd); 
    }
}

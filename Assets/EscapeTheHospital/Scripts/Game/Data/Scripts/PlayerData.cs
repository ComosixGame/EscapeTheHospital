using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int coin;
    public int skin;
    public int hat;
    public int shirt;
    public int pants;
    public int shoes;

    public PlayerData ()
    {
        level = 6;
        coin  = 0;
        skin  = 0;
        hat   = 0;
        shirt = 0;
        pants = 0;
        shoes = 0;
    }

    public void SaveData ()
    {
        GameDataManager<PlayerData>.SaveData(this);
    }

    public static PlayerData LoadData ()
    {
        return GameDataManager<PlayerData>.LoadData();
    }
}

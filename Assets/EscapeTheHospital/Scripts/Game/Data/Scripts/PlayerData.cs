using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int       currentCoins;
    public List<int> totalLevels;
    public List<int> totalSkins;
    public List<int> totalHats;
    public List<int> totalShirts;
    public List<int> totalPants;
    public List<int> totalShoes;

    public PlayerData ()
    {
        currentCoins  = 0;
        totalLevels = new List<int>();
        totalSkins  = new List<int>();
        totalHats   = new List<int>();
        totalShirts = new List<int>();
        totalPants  = new List<int>();
        totalShoes  = new List<int>();

        totalSkins.Add(0);
        totalHats.Add(0);
        totalShirts.Add(0);
        totalPants.Add(0);
        totalShoes.Add(0);
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

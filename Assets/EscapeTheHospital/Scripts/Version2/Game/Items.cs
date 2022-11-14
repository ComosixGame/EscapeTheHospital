using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class Items 
{
    public void DectectedLostKey(Vector3 pos)
    {
        GameManager.Instance.PlayerHaskKey(pos);
    }

    public void DetectedLostElectric(Vector3 pos)
    {
        GameManager.Instance.ElectricOff(pos);
    }


}

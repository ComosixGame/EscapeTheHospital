using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class Items 
{
    public UnityEvent<Vector3> OnDetectedTarget = new UnityEvent<Vector3>();
    public void DectectedLostKey(Vector3 pos)
    {
        // if ()
        // {
        //     OnDetectedTarget?.Invoke(pos);
        // }
    }
}

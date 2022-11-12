using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class Items 
{
    private Scanner scanner = new Scanner();
    private PlayerController playerController;
    public void DectectedLostKey(Vector3 pos)
    {
        GameManager.Instance.PlayerHaskKey(pos);
    }

    public void PlayerPickPoison()
    { 
        // scanner.PlayerCanInvisible();
    }

}

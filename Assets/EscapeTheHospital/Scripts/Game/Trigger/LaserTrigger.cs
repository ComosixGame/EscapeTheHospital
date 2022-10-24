using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    public int laserID;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
            GameManager.LaserOff(laserID);
    }
}

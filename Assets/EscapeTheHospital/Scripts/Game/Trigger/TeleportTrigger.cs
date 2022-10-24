using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public int triggerID;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            GameManager.TeleportPlayer(triggerID);
        }
    }
}

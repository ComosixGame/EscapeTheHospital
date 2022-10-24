using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public int triggerID;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && Key.isHasKey)
            GameManager.StartDoorEvent(triggerID);
    }
}

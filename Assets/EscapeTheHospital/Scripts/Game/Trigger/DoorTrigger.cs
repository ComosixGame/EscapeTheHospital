using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public int triggerID;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            GameEventManager.Instance.DoorwayTriggerEnter(triggerID);
            FindObjectOfType<AudioManager>().Play("OpenDoor");

            Destroy(this);
        }     
    }

    // private void OnTriggerExit(Collider other) 
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         GameEventManager.Instance.DoorwayTriggerExit(triggerID);
    //     }
    // }
}

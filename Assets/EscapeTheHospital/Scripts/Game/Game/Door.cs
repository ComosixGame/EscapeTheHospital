using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public int doorID;
    public Transform doorOpenPos;
    // public Transform doorClosePos;

    private void Start() 
    {
        GameEventManager.Instance.onDoorwayTriggerEnter += OnDoorwayOpen;
        // GameEventManager.Instance.onDoorwayTriggerExit += OnDoorwayClose; 
    }

    private void OnDoorwayOpen(int doorID)
    {
        if (doorID == this.doorID)
        {
            transform.position = doorOpenPos.position;
        }
    }

    // private void OnDoorwayClose(int doorID)
    // {
    //     if (doorID == this.doorID)
    //     {
    //         transform.position = new Vector3(-12.92f, 0, 12.08f);
    //     }
    // }

    // private void OnDestroy() 
    // {
    //     GameEventManager.Instance.onDoorwayTriggerEnter -= OnDoorwayOpen;
    //     // GameEventManager.Instance.onDoorwayTriggerExit -= OnDoorwayClose; 
    // }

    // private void OnDisable() 
    // {
    //     GameEventManager.Instance.onDoorwayTriggerEnter -= OnDoorwayOpen;
    // }
}

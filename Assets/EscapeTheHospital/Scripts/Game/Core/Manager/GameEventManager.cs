using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventManager : Singleton<GameEventManager>
{
    public event Action<int> onDoorwayTriggerEnter;

    public event Action onDocumentTriggerEnter;
    // public event Action<int> onDoorwayTriggerExit;

    // Start is called before the first frame update
    void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoorwayTriggerEnter(int id)
    {
        if (onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter(id);
        }
    }
    // public void DoorwayTriggerExit(int id)
    // {
    //     if (onDoorwayTriggerEnter != null)
    //     {
    //         onDoorwayTriggerExit(id);
    //     }
    // }

    public void TakeDocumentTriggerEnter()
    {   
        // if (onDocumentTriggerEnter != null)
        // {
            onDocumentTriggerEnter();
        // }
    }
}

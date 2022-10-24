using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    private bool _isOpen = false;
    public static bool isWin = false;
    public int doorID;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onOpenDoorEvent.AddListener(OpenDoor);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOpen)
        {
            //Do Something
        }
    }

    private void OpenDoor(int triggerID)
    {
        if (triggerID == doorID)
        {
            _isOpen = true;
            isWin = true;
        }
    }

    private void OnDisable() 
    {
        GameManager.onOpenDoorEvent.RemoveListener(OpenDoor); 
    }
}

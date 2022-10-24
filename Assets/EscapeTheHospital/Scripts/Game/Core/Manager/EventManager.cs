using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    public static UnityEvent<int> onOpenDoorEvent = new UnityEvent<int>();
    public static UnityEvent onPickUpKeyEvent = new UnityEvent();  
    public static UnityEvent<int> onEndEvent = new UnityEvent<int>();
    public static UnityEvent<int> onTeleportEvent = new UnityEvent<int>();
    public static UnityEvent<int> onLaserEvent = new UnityEvent<int>();

    private void Awake() 
    {
        base.Awake();
    }

    public static void TeleportPlayer(int id)
    {
        onTeleportEvent?.Invoke(id);
    }

    public static void LaserOff(int id)
    {
        onLaserEvent?.Invoke(id);
    }

    public static void PickedUpKey()
    {
        onPickUpKeyEvent?.Invoke();
    }

    public static void StartDoorEvent(int id)
    {
        onOpenDoorEvent?.Invoke(id);
    }

    public static void EndGame(int id)
    {
        onEndEvent?.Invoke(id);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{ 
    
    public Transform destination;
    public LayerMask layer;

    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.SetActive(false);
            other.transform.position = destination.position;
            Debug.Log(destination);
            other.gameObject.SetActive(true);
        }
    }
}

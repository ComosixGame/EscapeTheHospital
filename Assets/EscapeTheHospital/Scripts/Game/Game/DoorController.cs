using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform doorOpenPos;
    public LayerMask layer; 

    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        // if (other.CompareTag("Player"))
        {
            AudioManager.Instance.Play("OpenDoor");
            transform.position = doorOpenPos.position;

            Destroy(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LayerMask layer;
    private void OnTriggerEnter(Collider other) 
    { 
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log(111);
            // GameManager.Instance.EndGame();
        }

    }
}

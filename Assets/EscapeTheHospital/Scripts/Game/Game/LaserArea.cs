using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserArea : MonoBehaviour
{
    // public GameObject laserArea;
    public GameObject laserGO;
    public LayerMask layer;
    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            laserGO.SetActive(false);
            // laserArea.SetActive(false);
        }
    }
    
}

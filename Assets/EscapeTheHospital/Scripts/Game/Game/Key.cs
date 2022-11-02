using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static bool isHasKey = false;
    public LayerMask layer;
    public GameObject key;
    
    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            isHasKey = true;
            key.SetActive(false);
        }
        
    }

}

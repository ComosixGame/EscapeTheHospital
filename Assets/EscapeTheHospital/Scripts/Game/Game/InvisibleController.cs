using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleController : MonoBehaviour
{
    private GameObject _player;
    public GameObject position;
    public LayerMask layer;
    public Material material;

    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            other.GetComponentInChildren<Renderer>().material = material;
            Destroy(this);
        }
    }

}

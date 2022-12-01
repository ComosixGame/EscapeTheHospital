using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convenyor : MonoBehaviour
{
    public LayerMask layer;
    [SerializeField] private float speed = 0.005f;
    Rigidbody rb;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        pos = rb.position;             
    }

    private void OnTriggerStay(Collider other) {    
        if((layer & (1 << other.gameObject.layer)) != 0) {
            other.gameObject.transform.GetComponent<CharacterController>().Move(pos*speed);
        }
    }
}

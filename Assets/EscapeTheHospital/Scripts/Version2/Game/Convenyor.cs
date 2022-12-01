using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convenyor : MonoBehaviour
{
    public LayerMask layer;
    private CharacterController characterController;
    [SerializeField] private float speed = 1000f;
    Rigidbody rb;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update() 
    {
        pos = rb.position;              
    }

    private void OnTriggerStay(Collider other) {    
        if((layer & (1 << other.gameObject.layer)) != 0) {
            characterController.Move(pos*speed); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentTrigger : MonoBehaviour
{
     private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            GameEventManager.Instance.TakeDocumentTriggerEnter();
            
            Destroy(this);
        }     
    }
}

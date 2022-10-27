using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Document : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.Instance.onDocumentTriggerEnter += OnTakeDocument;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTakeDocument()
    {
        // if (documentID == this.documentID)
        // {
            gameObject.SetActive(false);
        // }
    }
}

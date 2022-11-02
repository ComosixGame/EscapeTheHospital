using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocManager : MonoBehaviour
{
    public LayerMask layer;
    [SerializeField] private int docs;

    private GameManager gameManager;
    private AudioManager audioManager;

    private void Awake() {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
    }
    
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.layer);
        if((layer & (1 << other.gameObject.layer)) != 0) {
            Debug.Log(123);
            // doc.SetActive(false);
            Destroy(gameObject);
            // soundManager.PlayOneShot(audioClip, volumeScale);
        } 
    }

    private void OnDestroy() {
        gameManager.UpdateDocs(docs);
    }
}

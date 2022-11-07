using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocManager : MonoBehaviour
{
    public LayerMask layer;
    [SerializeField] private int docs;

    private GameManager gameManager;
    private AudioManager _audioManager;
    public AudioClip audioClip;
    [Range(0,1)] public float volumeScale;

    private void Awake() {
        gameManager = GameManager.Instance;
        _audioManager = AudioManager.Instance;
    }
    
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.layer);
        if((layer & (1 << other.gameObject.layer)) != 0) {
            // doc.SetActive(false);
            _audioManager.PlayOneShot(audioClip, volumeScale);
            Destroy(gameObject);
            // soundManager.PlayOneShot(audioClip, volumeScale);
        } 
    }

    private void OnDestroy() {
        gameManager.UpdateDocs(docs);
    }
}

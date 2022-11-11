using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private AudioManager _audioManager;
    private GameManager gameManager;
    public static bool isHasKey = false;
    public LayerMask layer;
    public GameObject key;

    public AudioClip audioClip;

    [Range(0,1)] public float volumeScale;

    private void Awake() 
    {
        _audioManager = AudioManager.Instance;
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            _audioManager.PlayOneShot(audioClip, volumeScale);
            isHasKey = true;
            key.SetActive(false);
        }
        
    }

    private void OnDestroy() {
        isHasKey = false;
    }

}

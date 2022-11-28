using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserArea : MonoBehaviour
{
    public GameObject laserGO;
    public LayerMask layer;
    private AudioManager _audioManager;
    public AudioClip audioClip;
    [Range(0,1)] public float volumeScale;

    private void Awake() {
        _audioManager = AudioManager.Instance;
    }
    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            _audioManager.PlayOneShot(audioClip, volumeScale);
            laserGO.SetActive(false);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LayerMask layer;

    private AudioManager _audioManager;
    public AudioClip audioClip;
    [Range(0,1)] public float volumeScale;

    private GameManager _gameManager;
    private void Awake() 
    {
        _gameManager = GameManager.Instance;
        _audioManager = AudioManager.Instance;
    }
    private void OnTriggerEnter(Collider other) 
    { 
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            _audioManager.PlayOneShot(audioClip, volumeScale);
            _gameManager.EndGame(false);
        }

    }
}

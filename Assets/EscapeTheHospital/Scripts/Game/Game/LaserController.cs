using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LayerMask layer;
    public GameObject effectLose;
    private Vector3 pos;

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
            pos.x = other.transform.position.x;
            pos.y = 10f;
            pos.z = other.transform.position.z;
            effectLose.transform.position = pos;
            effectLose.SetActive(true);
            _gameManager.EndGame(false);
        }

    }
}

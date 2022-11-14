using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LayerMask layer;
    public AudioClip audioClip;
    [Range(0,1)] public float volumeScale;
    private AudioManager audioManager;
    private GameManager gameManager;

    private void Awake() 
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            audioManager.PlayOneShot(audioClip, volumeScale);
            gameManager.EndGame(false);
        }
    }
}

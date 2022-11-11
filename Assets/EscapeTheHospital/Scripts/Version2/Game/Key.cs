using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Items items = new Items();
    private AudioManager audioManager;
    public  LayerMask layer;
    public  GameObject key;
    public AudioClip audioClip;
    [Range(0,1)] public float volumeScale;

    private void Awake() 
    {
        audioManager = AudioManager.Instance;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            // audioManager.PlayOneShot(audioClip, volumeScale);
            items.DectectedLostKey(key.transform.position);
            key.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Invisible : MonoBehaviour
{
    private Items items = new Items();
    public GameObject poison;
    public LayerMask layer;
    public Material material;
    [Range(0,1)] public float volumeScale;
    public AudioClip audioClip;
    private AudioManager audioManager;

    private void Awake() 
    {
        audioManager = AudioManager.Instance;
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            items.PlayerPickPoison();
            audioManager.PlayOneShot(audioClip, volumeScale);
            other.GetComponentInChildren<Renderer>().material = material;
            poison.SetActive(false);
        }
    }
}

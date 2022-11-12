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
    private int LayerIgnore;

    private void Awake() 
    {
        audioManager = AudioManager.Instance;
        LayerIgnore = LayerMask.NameToLayer("Invisible");
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.layer = LayerIgnore;
            audioManager.PlayOneShot(audioClip, volumeScale);
            other.GetComponentInChildren<Renderer>().material = material;
            poison.SetActive(false);
        }
    }
}

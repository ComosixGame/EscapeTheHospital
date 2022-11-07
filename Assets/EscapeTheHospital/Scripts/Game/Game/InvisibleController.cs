using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleController : MonoBehaviour
{
    public GameObject position;
    public LayerMask layer;
    public Material material;
    public static bool isVisible = false;

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
            other.GetComponentInChildren<Renderer>().material = material;
            isVisible = true;
            position.SetActive(false);
        }
    }

    private void OnDestroy() {
        isVisible = false;
    }

}

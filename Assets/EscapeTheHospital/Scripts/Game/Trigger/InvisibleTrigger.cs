using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTrigger : MonoBehaviour
{
    public GameObject poison;
    public delegate void InvisiblePlayer();
    public static event InvisiblePlayer onInvisiblePlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            poison.SetActive(false);
            onInvisiblePlayer?.Invoke();
        }
    }
}

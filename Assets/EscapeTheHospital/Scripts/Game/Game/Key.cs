using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static bool isHasKey;
    private void Start() 
    {
        GameManager.onPickUpKeyEvent.AddListener(HasKey);
    }

    private void HasKey()
    {
        isHasKey = true;
    }
    
    private void OnDisable() 
    {
        GameManager.onPickUpKeyEvent.RemoveListener(HasKey);
    }

}

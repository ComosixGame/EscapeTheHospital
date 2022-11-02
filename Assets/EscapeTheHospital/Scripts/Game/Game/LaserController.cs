using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LayerMask layer;

    private GameManager _gameManager;
    private void Awake() 
    {
        _gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter(Collider other) 
    { 
        if ((layer & (1 << other.gameObject.layer)) != 0)
        {
            _gameManager.EndGame(false);
        }

    }
}

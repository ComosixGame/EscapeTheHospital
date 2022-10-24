using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public int poisonID;
    private bool _isInvisible = false;
    [SerializeField] private GameObject _poisonGO;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.onPickPoisonEvent.AddListener(InvisiblePlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInvisible)
        {
            _poisonGO.SetActive(false);
            //Do Something
        }
    }

    private void InvisiblePlayer(int triggerID)
    {
        if (triggerID == poisonID)
        {
            _isInvisible = true;
        }
    }

    private void OnDisable() 
    {
        GameManager.onPickPoisonEvent.AddListener(InvisiblePlayer);
    }
}

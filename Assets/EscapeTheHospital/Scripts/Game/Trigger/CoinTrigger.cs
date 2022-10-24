using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    public int coins;
    public GameObject coinsGO;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.UpdateCurrency(coins);
            coinsGO.SetActive(false);
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Coin : MonoBehaviour
// {
//     public int coins;
//     public GameObject c_GO;

//     private bool _isPick = false;
//     // Start is called before the first frame update
//     void Start()
//     {
//         GameManager.Instance.onUpdateCoins.AddListener(PickUpCoin);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//     }

//     private void OnTriggerEnter(Collider other) 
//     {
//         if (other.CompareTag("Player"))
//         {
//             PickUpCoin();
//         }
//     }

//     private void PickUpCoin (int c)
//     { 
//         _isPick = true;
//     }
// }

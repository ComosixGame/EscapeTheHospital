// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Laser : MonoBehaviour
// {
//     public int laserID;
//     private bool _isOff = false;
//     [SerializeField] private GameObject _laserArea;
//     [SerializeField] private GameObject _lightSaber;

//     // Start is called before the first frame update
//     void Start()
//     {
//         GameManager.onLaserEvent.AddListener(LaserOff);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (_isOff)
//         {
//             _laserArea.SetActive(false);
//             _lightSaber.SetActive(false);
//         }
//     }

//     private void LaserOff(int triggerID)
//     {
//         if (triggerID == laserID)
//         {
//             _isOff = true;
//         }
//     }

//     private void OnDisable() 
//     {
//         GameManager.onLaserEvent.RemoveListener(LaserOff);
//     }
// }

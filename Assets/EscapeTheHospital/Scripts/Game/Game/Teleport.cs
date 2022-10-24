// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Teleport : MonoBehaviour
// {
//     public int teleportID;
//     private bool _isOn = false;
//     [SerializeField] private GameObject _playerGO;
//     [SerializeField] private Transform _destination;
//     [SerializeField] private Transform _playerPos;

//     // Start is called before the first frame update
//     void Start()
//     {
//         GameManager.onTeleportEvent.AddListener(PlayerTeleport);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (_isOn)
//         {
//             _playerGO.SetActive(false);
//             _playerPos.position = _destination.position;
//             _playerGO.SetActive(true);
//         }
        
//     }

//     private void PlayerTeleport(int triggerID)
//     {
//         if (triggerID == teleportID)
//         {
//             _isOn = true;
//         }
//     }

//     private void OnDisable() 
//     {
//         GameManager.onLaserEvent.RemoveListener(PlayerTeleport);
//     }
// }

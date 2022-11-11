using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DoorController : MonoBehaviour
{
    public Vector3 doorOpenPos;
    public LayerMask layer; 
    public AudioClip audioClip;
    private AudioManager _audioManager;
    private GameManager gameManager;
    private bool isHasKey;

    public Transform door;
    private bool isTrigger, opened;
    [Range(0,1)] public float volumeScale;

    private void Awake() 
    {
        _audioManager = AudioManager.Instance;
    }

    private void OnEnable() 
    {
        gameManager.onPlayerHasKey.AddListener(PlayerHasKey);
    }

    private void Update() {
        if(isTrigger) {
            Vector3 doorPos = door.position;
            if(!opened) {
                door.position = Vector3.MoveTowards(doorPos, doorOpenPos, 10f * Time.deltaTime);
                if(Vector3.Distance(doorPos, doorOpenPos) <= 0) {
                    opened = true;
                }
            }
        }
    }

    private void PlayerHasKey(Vector3 pos)
    {
        isHasKey = true;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(isTrigger) return;
        if ((layer & (1 << other.gameObject.layer)) != 0 && isHasKey)
        // if (other.CompareTag("Player"))
        {
            isTrigger =  true;
            _audioManager.PlayOneShot(audioClip, volumeScale);
        }
    }

    private void OnDisable() 
    {
        gameManager.onPlayerHasKey.RemoveListener(PlayerHasKey);
        // isHasKey = false;
    }
    

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(doorOpenPos, 1f);
        Handles.DrawDottedLine(door.position, doorOpenPos, 5f);
    }

    [CustomEditor(typeof(DoorController))]
    public class EditorDoorController : Editor {

        private void OnSceneGUI() {
            DoorController doorController = target as DoorController;
            EditorGUI.BeginChangeCheck();
            Vector3 pos = Handles.PositionHandle(doorController.doorOpenPos, Quaternion.identity);
            if(EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(doorController, "change doorOpenPos");
                doorController.doorOpenPos = pos;
            }
        }


        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            DoorController doorController = target as DoorController;
            EditorGUI.BeginChangeCheck();
            if(GUILayout.Button("Reset doorOpenPos")) {
                if(EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(doorController, "change doorOpenPos");
                    doorController.doorOpenPos = doorController.door.position;
                }
            }
            
        }
    }
#endif
}

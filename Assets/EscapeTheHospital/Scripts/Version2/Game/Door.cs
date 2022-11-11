using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Door : MonoBehaviour
{
    public Vector3 doorOpenPos;
    public LayerMask layer;
    public AudioClip audioClip;
    public Transform door;
    private AudioManager audioManager;
    private GameManager gameManager;
    private bool isTrigger, isOpend, isHasKey;
    [Range(0,1)] public float volumeScale;

    private void Awake() 
    {
        audioManager = AudioManager.Instance;
        gameManager = GameManager.Instance;
    }

    private void OnEnable() 
    {
        gameManager.OnDetectedLostkey.AddListener(DoorHasKey);
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrigger) {
            Vector3 doorPos = door.position;
            if(!isOpend) {
                door.position = Vector3.MoveTowards(doorPos, doorOpenPos, 10f * Time.deltaTime);
                if(Vector3.Distance(doorPos, doorOpenPos) <= 0) {
                    isOpend = true;
                }
            }
        }  
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (isTrigger) return;
        if ((layer & (1 << other.gameObject.layer)) != 0 && isHasKey)
        {
            audioManager.PlayOneShot(audioClip, volumeScale);
            isTrigger = true;
        }
    }

    private void DoorHasKey(Vector3 pos)
    {
        Debug.Log("1234A");
        isHasKey = true;
    }

    private void OnDisable() 
    {
        gameManager.OnDetectedLostkey.RemoveListener(DoorHasKey);
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

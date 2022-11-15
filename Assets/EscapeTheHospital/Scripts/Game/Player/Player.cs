using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour
{
    public Scripables scriptablePlayer;
    public CinemachineFreeLook cmFL;
    public RectTransform joyStick;
    private GameObject _player;
    private GameObject player1;
    private GameObject _player2;
    public GameObject poisonCloud;
    public GameObject poison;

    private void OnEnable() 
    {
        GameManager.Instance.OnZoombieDetected.AddListener(ChangeZoombie);
    }

    // Start is called before the first frame update
    void Start()
    {   
        //Choice Player
        _player = scriptablePlayer.scriptableObjects[0].obj;
        //Choice Skin & Items
        //Do Something
        Vector3 positionPlayer = transform.position;
        GameObject _playerSpawn = Instantiate(_player,positionPlayer,Quaternion.identity);
        cmFL.Follow = _playerSpawn.transform;
        cmFL.LookAt = _playerSpawn.transform;    
        _playerSpawn.GetComponent<PlayerController>().joystickRectTrans = joyStick;
        player1 = _playerSpawn;
    }

    private void ChangeZoombie(Vector3 pos)
    {
        player1.SetActive(false);
        _player2 = scriptablePlayer.scriptableObjects[1].obj;
        GameObject _playerSpawn = Instantiate(_player2,pos,Quaternion.identity);
        poison.transform.position = pos;
        poisonCloud.transform.position = pos;
        poison.SetActive(true);
        poisonCloud.SetActive(true);
        GameManager.Instance.OnZoombieDetected.RemoveListener(ChangeZoombie);
    }
}

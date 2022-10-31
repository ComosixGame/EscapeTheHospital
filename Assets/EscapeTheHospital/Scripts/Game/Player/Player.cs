using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public Scripables scriptablePlayer;
    public CinemachineFreeLook cmFL;
    public GameObject positionSpawn;
    public RectTransform joyStick;
    private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {   
        //Choice Player
        _player = scriptablePlayer.scriptableObjects[0].obj;
        //Choice Skin & Items
        //Do Something
        Vector3 positionPlayer = positionSpawn.transform.position;
        GameObject _playerSpawn = Instantiate(_player,positionPlayer,Quaternion.identity);
        cmFL.Follow = _playerSpawn.transform;
        cmFL.LookAt = _playerSpawn.transform;    
        _playerSpawn.GetComponent<PlayerController>().joystickRectTrans = joyStick;
    }
}

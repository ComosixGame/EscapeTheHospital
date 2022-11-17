using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour
{
    public Scripables scriptablePlayer;
    public CinemachineFreeLook cmFL;
    public RectTransform joyStick;
    private GameObject _player;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {   
        //Choice Player
        _player = scriptablePlayer.scriptableObjects[0].obj;
        //Choice Skin & Items
        //Do Something
        Vector3 positionPlayer = transform.position;
        GameObject _playerSpawn = Instantiate(_player,positionPlayer,Quaternion.identity);
        playerTransform = _playerSpawn.transform;
        cmFL.Follow = playerTransform;
        cmFL.LookAt = playerTransform;    
        _playerSpawn.GetComponent<PlayerController>().joystickRectTrans = joyStick;
    }

}

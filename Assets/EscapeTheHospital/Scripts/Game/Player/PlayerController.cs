using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private PlayerInputActions _pInput;
    private Animator _pAnimator;
    private Vector2 _inputMove;
    private float _velocity;
    private int _velocityHash;
    [SerializeField] private float _speed = 10f;
    private CharacterController _cController;
    public RectTransform joystickRectTrans;  

    private void Awake() 
    {
        _cController = GetComponent<CharacterController>();
        _pInput = new PlayerInputActions();

        _pInput.Player.Move.performed += SetDirMove;
        _pInput.Player.Move.canceled += SetDirMove;
    }
    // Start is called before the first frame update
    void Start()
    {
        _pAnimator = GetComponent<Animator>();
        _velocityHash = Animator.StringToHash("Velocity");
        joystickRectTrans.position = new Vector2(9999999, 9999999);
    }

    private void OnEnable() 
    {
        _pInput.Enable();
        _pInput.Player.StartTouch.performed += ShowJoystick;
        _pInput.Player.HoldTouch.canceled += HideJoystick;
    }

    private void Update() 
    {
        if (new Vector3(_inputMove.x,0,_inputMove.y) != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(_inputMove.x,0,_inputMove.y)*Time.deltaTime);
        }   
            _cController.Move(new Vector3(_inputMove.x,0,_inputMove.y)*_speed*Time.deltaTime);
            _pAnimator.SetFloat(_velocityHash, _velocity);
    }

    private void SetDirMove(InputAction.CallbackContext ctx)
    {
        _inputMove = ctx.ReadValue<Vector2>();
        _velocity = Vector3.Distance(Vector3.zero, new Vector3(_inputMove.x,0,_inputMove.y));
    }

       private void ShowJoystick(InputAction.CallbackContext ctx) {
        // if(!isPause && isStart) {
            joystickRectTrans.position = ctx.ReadValue<Vector2>();
        // }
    }

    
    private void HideJoystick(InputAction.CallbackContext ctx) {
        //hide joystick out of UI view
        joystickRectTrans.position = new Vector2(9999999, 9999999);
    }

    private void OnDisable()
    {
        _pInput.Player.Move.performed -= SetDirMove;
        _pInput.Player.Move.canceled -= SetDirMove;

        _pInput.Player.StartTouch.performed -= ShowJoystick;
        _pInput.Player.HoldTouch.canceled -= HideJoystick;
    }
}

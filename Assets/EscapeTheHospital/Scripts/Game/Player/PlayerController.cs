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
    private Vector3 _dirMove;
    private CharacterController _cController;
    private float _fallingVelocity;
    public RectTransform joystickRectTrans;  
    public float gravity = -9.81f;

    private void Awake() 
    {
        _cController = GetComponent<CharacterController>();
        _pInput = new PlayerInputActions();
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

        _pInput.Player.Move.performed += GetDirMove;
        _pInput.Player.Move.canceled += GetDirMove;
        _pInput.Player.StartTouch.performed += ShowJoystick;
        _pInput.Player.HoldTouch.canceled += HideJoystick;
    }

    private void Update() 
    {
        MovePlayer();
        RotationLook();
        HandlAnimation();
        HandleGravity();
    }
    private void MovePlayer()
    {
        Vector3 motionMove = _dirMove * _speed * Time.deltaTime;
        Vector3 motionFall = Vector3.up * _fallingVelocity * Time.deltaTime;
        _cController.Move(motionMove + motionFall);
    }

    private void RotationLook()
    {
        if (_dirMove != Vector3.zero)
        {
            Quaternion rotLook = Quaternion.LookRotation(_dirMove);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotLook, 20f * Time.deltaTime);
        }
    }
    private void GetDirMove(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        _dirMove = new Vector3(dir.x, 0, dir.y);
    }

       private void ShowJoystick(InputAction.CallbackContext ctx) {
        // if(!isPause && isStart) {
            joystickRectTrans.position = ctx.ReadValue<Vector2>();
        // }
    }

    private void HandleGravity() {
        if(_cController.isGrounded) {
            _fallingVelocity = gravity/10;
        } else {
            _fallingVelocity += gravity/10;
        }
    }

    private void HandlAnimation() {
        Vector3 horizontalVelocity = new Vector3(_cController.velocity.x, 0, _cController.velocity.z);
        float Velocity = horizontalVelocity.magnitude/_speed;
        if(Velocity > 0) {
            _pAnimator.SetFloat(_velocityHash, Velocity);
        } else {
            float v = _pAnimator.GetFloat(_velocityHash);
            v = v> 0.01f ? Mathf.Lerp(v, 0, 20f * Time.deltaTime): 0;
            _pAnimator.SetFloat(_velocityHash, v);
        }
    }

    
    private void HideJoystick(InputAction.CallbackContext ctx) {
        //hide joystick out of UI view
        joystickRectTrans.position = new Vector2(9999999, 9999999);
    }

    private void OnDisable()
    {
        _pInput.Player.Move.performed -= GetDirMove;
        _pInput.Player.Move.canceled -= GetDirMove;
        _pInput.Player.StartTouch.performed -= ShowJoystick;
        _pInput.Player.HoldTouch.canceled -= HideJoystick;
    }
}

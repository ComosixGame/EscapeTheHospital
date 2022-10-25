using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController _cController;
    private PlayerInputActions _pInput;
    private Animator _pAnimator;
    private Vector2 _inputMove;
    private float _velocity;
    private int _velocityHash;
    [SerializeField] private float _speed = 0.1f;

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
    }

    private void OnEnable() 
    {
        _pInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        if (new Vector3(_inputMove.x,0,_inputMove.y) != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(_inputMove.x,0,_inputMove.y));
        }
            _cController.Move(new Vector3(_inputMove.x,0,_inputMove.y)*_speed);
            _pAnimator.SetFloat(_velocityHash, _velocity);
    }

    private void SetDirMove(InputAction.CallbackContext ctx)
    {
        _inputMove = ctx.ReadValue<Vector2>();
        _velocity = Vector3.Distance(Vector3.zero, new Vector3(_inputMove.x,0,_inputMove.y));
    }

    private void OnDisable() 
    {
        _pInput.Disable();
    }
}

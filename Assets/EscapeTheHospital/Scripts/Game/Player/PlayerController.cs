using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController _cController;
    private PlayerInputActions _pInput;
    private Vector2 _inputMove;
    private float _velocity;
    [SerializeField] private float _speed = 0.01f;

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
            _cController.Move(new Vector3(_inputMove.x,0,_inputMove.y)*_speed);
        }
    }

    private void SetDirMove(InputAction.CallbackContext ctx)
    {
        _inputMove = ctx.ReadValue<Vector2>();
    }

    private void OnDisable() 
    {
        _pInput.Disable();
    }
}

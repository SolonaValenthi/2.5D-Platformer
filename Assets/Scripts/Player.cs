using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravityForce;
    [SerializeField] private float _jumpForce;

    private int _coins;
    private float _yVelocity;
    private bool _doubleJumpReady = true;
    private PlayerInputActions _input;
    private CharacterController _controller;


    public static Action<int> OnCollectCoin;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = new PlayerInputActions();
        _input.Player.Enable();
        _input.Player.Jump.performed += Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (_controller.isGrounded)
        {
            _yVelocity = _jumpForce;
        }
        else if (_doubleJumpReady)
        {
            _yVelocity = _jumpForce;
            _doubleJumpReady = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        float move = _input.Player.Movement.ReadValue<float>();
        Vector3 velocity = new Vector3(move, 0, 0) * _moveSpeed;
        
        if (!_controller.isGrounded)
        {
            _yVelocity -= _gravityForce * Time.deltaTime;
        }

        if (_controller.isGrounded && _doubleJumpReady == false)
        {
            _doubleJumpReady = true;
        }

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    public void CollectCoin()
    {
        _coins++;
        OnCollectCoin?.Invoke(_coins);
    }
}

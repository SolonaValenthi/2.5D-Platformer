using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravityForce;
    [SerializeField] private float _jumpForce;

    private float _yVelocity;
    private PlayerInputActions _input;
    private CharacterController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = new PlayerInputActions();
        _input.Player.Enable();
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

        if (_controller.isGrounded)
        {
            if (_input.Player.Jump.WasPressedThisFrame())
            {
                _yVelocity = _jumpForce;
            }
        }
        else
        {
            _yVelocity -= _gravityForce * Time.deltaTime;
        }

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }
}

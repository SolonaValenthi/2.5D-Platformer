using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravityForce;
    [SerializeField] private float _jumpForce;
    [SerializeField] private CinemachineVirtualCamera _vCam;

    private int _coins;
    private int _lives = 3;
    private float _yVelocity;
    private bool _doubleJumpReady = true;
    private PlayerInputActions _input;
    private CharacterController _controller;

    public static Action<int> OnCollectCoin;
    public static Action<int> OnLivesChanged;

    Vector3 _spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _spawnPoint = transform.position;
        _input = new PlayerInputActions();
        _input.Player.Enable();
        _input.Player.Jump.performed += Jump;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Deathzone"))
        {
            PlayerDeath();
        }
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

    public void CollectCoin()
    {
        _coins++;
        OnCollectCoin?.Invoke(_coins);
    }

    private void PlayerDeath()
    {
        _lives--;
        OnLivesChanged?.Invoke(_lives);

        if (_lives > 0)
            Respawn();
        else
            Destroy(this.gameObject);
    }

    public void Respawn()
    {
        _controller.enabled = false;
        transform.position = _spawnPoint;
        _controller.enabled = true;
        _yVelocity = 0;
    }

    private void OnDisable()
    {
        _input.Player.Jump.performed -= Jump;
    }
}

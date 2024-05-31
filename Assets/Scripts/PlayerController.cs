using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    private CharacterController _characterController;
    private Vector3 _velocity;
    private bool _isGrounded;

    private float _speed = 6.0f;
    private float _originalSpeed;
    private float _jumpHeight = 1.5f;
    private float _gravity = -9.81f;

    private int _jumpCount = 0;
    private int _maxJumpCount = 1;
      
    private bool _doubleJumpEnabled = false;
    private float _doubleJumpDuration = 10f;
    private float _doubleJumpTimer = 0f;

    private bool _speedBoostEnabled = false;
    private float _speedBoostDuration = 10f;
    private float _speedBoostTimer = 0f;
          

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _originalSpeed = _speed;
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
            _jumpCount = 0;
        }

        Move();
        Jump();
        DoubleJump();
        TurningDoubleJumpTimer();
        TurningSpeedBoostTimer();

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
 

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = _cameraTransform.right * moveX + _cameraTransform.forward * moveZ;
        move.y = 0f;

        _characterController.Move(move * _speed * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            _jumpCount++;
        }
    }

    private void DoubleJump()
    {
        if (_doubleJumpEnabled && !_isGrounded && Input.GetButtonDown("Jump") && _jumpCount < _maxJumpCount)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            _jumpCount++;
        }
    }

    /* BONUS METHODS*/

    public void EnableDoubleJump()
    {
        _doubleJumpEnabled = true;
        _maxJumpCount = 2;
        _doubleJumpTimer = _doubleJumpDuration; 
    }

    public void DisableDoubleJump()
    {
        _doubleJumpEnabled = false;
        _maxJumpCount = 1;
    }

    public void EnableSpeedBoost()
    {
        _speed *= 2;
        _speedBoostEnabled = true;
        _speedBoostTimer = _speedBoostDuration;
    }

    public void DisableSpeedBoost()
    {
        _speed = _originalSpeed;
        _speedBoostEnabled = false;
        _speedBoostTimer = 0f;
    }

    private void TurningDoubleJumpTimer()
    {
        if (_doubleJumpEnabled)
        {
            _doubleJumpTimer -= Time.deltaTime;
            if (_doubleJumpTimer <= 0f)
            {
                DisableDoubleJump();
            }
        }
    }

    private void TurningSpeedBoostTimer()
    {
        if (_speedBoostEnabled)
        {
            _speedBoostTimer -= Time.deltaTime;
            if (_speedBoostTimer <= 0f)
            {
                DisableSpeedBoost();
            }
        }
    }
}

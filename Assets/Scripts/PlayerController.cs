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
    private float _jumpHeight = 1.5f;
    private float _gravity = -9.81f; // -9.81f the closest value of the acceleration of free fall on the Earth
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        Move();
        Jump();

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
        }
    }
}

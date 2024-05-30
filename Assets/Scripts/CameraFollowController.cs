using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _playerTransform;
    private float _distance = 5.0f;
    
    private float _sensitivity = 2.5f; 
    private float _minAngleY = -40f;
    private float _maxAngleY = 80f;

    private float _currentX = 0.0f;
    private float _currentY = 0.0f;

    private void Start()
    {
        _offset = new Vector3(0, 2 - _distance);
    }

    private void Update()
    {
        _currentX += Input.GetAxis("Mouse X") * _sensitivity;
        _currentY += Input.GetAxis("Mouse Y") * _sensitivity;
        _currentY = Mathf.Clamp(_currentY, _minAngleY, _maxAngleY);  //limit angels up and down
    }

    private void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);
        transform.position = _playerTransform.position + rotation * _offset;
        transform.LookAt(_playerTransform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
 
    [SerializeField] private Transform _playerTransform;
    private Vector3 _offset;

    private float _sensitivity = 2.5f; 
    private float _minAngleY = -75f;
    private float _maxAngleY = 75f;                    

    private float _currentX = 0.0f;
    private float _currentY = 0.0f;

    //private float _distance = 5.0f;

    private void Start()
    {
        _offset = new Vector3(4.5f, 2.5f, 0.5f);
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
        Vector3 position = _playerTransform.position + rotation * new Vector3(_offset.x, 0, _offset.z);
        position.y += _offset.y;
        transform.position = position;
        transform.LookAt(_playerTransform.position + Vector3.up * 1.5f);
    }
}

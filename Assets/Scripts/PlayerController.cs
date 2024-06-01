using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private TextMeshProUGUI _doubleJumpText;
    [SerializeField] private TextMeshProUGUI _speedBoostText;
    [SerializeField] private TextMeshProUGUI _currentSpeed;
    [SerializeField] private TextMeshProUGUI _heightAboveGround;
    [SerializeField] private ParticleSystem _bonusEffect;

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

    private Coroutine _doubleJumpCoroutine;
    private Coroutine _speedBoostCoroutine;
    private Coroutine _flipCoroutine;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _originalSpeed = _speed;
        _bonusEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
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
        ShowUI();
        ShowHeightAboveGround();

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

        if (move != Vector3.zero)
        {
            _currentSpeed.text = _speedBoostEnabled ? "Speed: 12" : "Speed: 6";
        }
        else
        {
            _currentSpeed.text = "Speed: 0";
        }
    }

     private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            _jumpCount++;
            if (_flipCoroutine != null)
            {
                StopCoroutine(_flipCoroutine);
            }
            _flipCoroutine = StartCoroutine(Flip());
        }
    }

    private void DoubleJump()
    {
        if (_doubleJumpEnabled && !_isGrounded && Input.GetButtonDown("Jump") && _jumpCount < _maxJumpCount)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            _jumpCount++;
            if (_flipCoroutine != null)
            {
                StopCoroutine(_flipCoroutine);
            }
            _flipCoroutine = StartCoroutine(Flip());
        }
    }

    private void ShowUI()
    {
        _doubleJumpText.text = _doubleJumpEnabled ? $"Double Jump: {_doubleJumpTimer:F1}s" : "";
        _speedBoostText.text = _speedBoostEnabled ? $"Speed Boost: {_speedBoostTimer:F1}s" : "";        
    }
    private void ShowHeightAboveGround()
    {
        RaycastHit hit;
        float _distanceToGround = 0f;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            _distanceToGround = hit.distance;

            //the variant bellow is for the distance to the main ground, and not to any solid object     

            /*if (hit.collider.CompareTag("Ground"))
            {
                distanceToGround = hit.distance;
            }
            else
            {

                distanceToGround = 0f;
            }*/
        }

        _heightAboveGround.text = $"Height: {_distanceToGround:F1}m";
    }


    private IEnumerator Flip()
    {
        float _rotationAmount = 0f;
        float _rotationSpeed = 360f / 0.5f;  //rotate on 0.5 sec

        Quaternion _initialRotation = transform.rotation;

        while (_rotationAmount < 360f)
        {
            float _rotationStep = _rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, _rotationStep, Space.Self);
            _rotationAmount += _rotationStep;
            yield return null;
        }

        transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
    }


    /* BONUS METHODS*/


    public void EnableDoubleJump()          //activate double jump bonus
    {
        if (_doubleJumpCoroutine != null)
        {
            StopCoroutine(_doubleJumpCoroutine);
        }
        _doubleJumpCoroutine = StartCoroutine(DoubleJumpEffect());
    }

    private IEnumerator DoubleJumpEffect()     //manage time double jump bonus
    {
        _doubleJumpEnabled = true;
        _maxJumpCount = 2;
        _doubleJumpTimer = _doubleJumpDuration;
        _bonusEffect.Play();

        while (_doubleJumpTimer > 0)
        {
            _doubleJumpTimer -= Time.deltaTime;
            yield return null;
        }

        DisableDoubleJump();
    }

    public void DisableDoubleJump()     //disable double jump bonus
    {
        _doubleJumpEnabled = false;
        _maxJumpCount = 1;
        _doubleJumpTimer = 0f;
        _bonusEffect.Stop();
    }

    public void EnableSpeedBoost()          //activate boost speed bonus
    {
        if (_speedBoostCoroutine != null)
        {
            StopCoroutine(_speedBoostCoroutine);
        }
        _speedBoostCoroutine = StartCoroutine(SpeedBoostEffect());
    }

    private IEnumerator SpeedBoostEffect()      //manage time boost speed bonus
    {
        _speed = _originalSpeed * 2;
        _speedBoostEnabled = true;
        _speedBoostTimer = _speedBoostDuration;
        _bonusEffect.Play();

        while (_speedBoostTimer > 0)
        {
            _speedBoostTimer -= Time.deltaTime;
            yield return null;
        }

        DisableSpeedBoost();
    }

    public void DisableSpeedBoost()    //disable boost speed bonus
    {
        _speed = _originalSpeed;
        _speedBoostEnabled = false;
        _speedBoostTimer = 0f;
        _bonusEffect.Stop();
    }
}

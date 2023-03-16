using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    float _playerHeight;
    UnityEvent _flyingEvent;
    UnityEvent _landedEvent;

    [Header("References")]
    float _jumpDuration;
    public float _flightDuration = 5f;
    public Text flightTimeText;

    [Header("Movement Speeds")]
    public float movementSpeed = 10f;
    public float jumpSpeed = 5f;
    float gravity = 9.8f;

    [Header("Surface Alignment")]
    public LayerMask groundLayer;

    Vector3 _movementDirection;
    Vector3 _rotationChange;

    CharacterController _charC;

    private void Start()
    {
        _charC = GetComponent<CharacterController>();
        _playerHeight = _charC.height;

        if (_flyingEvent == null)
        {
            _flyingEvent = new UnityEvent();
        }

        _flyingEvent.AddListener(FlyMovement);
        
        if (_landedEvent == null)
        {
            _landedEvent = new UnityEvent();
        }

        _landedEvent.AddListener(MovePlayer);

        _rotationChange = new Vector3(0, 45, 0);
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (IsGrounded())
        {
            _jumpDuration = 0;
            _flightDuration = 5;

            float horizontaInput = Input.GetAxis("Horizontal");

            _movementDirection = transform.TransformDirection(new Vector3(horizontaInput, 0, 0)) * movementSpeed;

            //if player presses forward button, rotates player model 45 degrees to the left
            if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
            {
                transform.Rotate(-_rotationChange);
            }
            else if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)
            {
                transform.Rotate(_rotationChange);
            }

            if (Input.GetButton("Jump"))
            {
                _movementDirection.y = jumpSpeed;
            }
        }
        else
        {
            if (Input.GetButton("Jump"))
            {
                _jumpDuration += Time.deltaTime;
                
                if (!IsGrounded() && _jumpDuration >= 1f)
                {
                    _flyingEvent.Invoke();
                }
            }
        }

        _movementDirection.y -= gravity * Time.deltaTime;
        _charC.Move(_movementDirection * Time.deltaTime);
    }

    public void FlyMovement()
    {
        _movementDirection.y = 0;

        _flightDuration -= Time.deltaTime;
        flightTimeText.text = $"Flight Time: {_flightDuration:00.0}";
        
        float horizontaInput = Input.GetAxis("Horizontal");

        _movementDirection = transform.TransformDirection(new Vector3(horizontaInput, 0, 0)) * movementSpeed;

        //if player presses forward button, rotates player model 45 degrees to the left
        if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
        {
            transform.Rotate(-_rotationChange);
        }
        else if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)
        {
            transform.Rotate(_rotationChange);
        }

        //pressing Q will levitate player upwards, while pressing E does opposite
        if (Input.GetKey(KeyCode.Q))
        {
            _movementDirection.y += 5;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            _movementDirection.y -= 5;
        }

        if (!IsGrounded() && _flightDuration <= 0)
        {
            _movementDirection.y -= gravity * 100f;
        }
    }

    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.1f, groundLayer))
        {
            return true;
        }

        return false;
    }
}


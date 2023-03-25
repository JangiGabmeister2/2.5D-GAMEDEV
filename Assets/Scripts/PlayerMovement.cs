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
    float _flightDuration = 5f;
    [Tooltip("Input text game object which shows how long the player has left before becoming unable to continue flying.")]
    public Text flightTimeText;
    public bool canFly = true;

    [Header("Movement Speeds")]
    [Tooltip("Input how fast the player can move.")]
    public float movementSpeed = 10f;
    [Tooltip("Input how much the player can jump.")]
    public float jumpSpeed = 5f;
    float gravity = 9.8f;

    [Header("Surface Alignment"), Tooltip("Input the ground layer.")]
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

        _landedEvent.AddListener(GroundedMovement);

        _rotationChange = new Vector3(0, 45, 0);
    }

    private void Update()
    {
        GroundedMovement();
    }

    public void MovePlayer()
    {
        //gets float value when player presses left or right buttons
        float horizontaInput = Input.GetAxis("Horizontal");

        //sets the direction of player movement according to above float value on the x-axis * movement speed
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
    }

    private void GroundedMovement()
    {
        //if player is touching the ground
        if (IsGrounded())
        {
            //resets duration of jump and flight to 0 and 5 respectively
            _jumpDuration = 0;
            _flightDuration = 5;

            MovePlayer();

            //if player presses jump button
            if (Input.GetButton("Jump"))
            {
                //sets the upward direction to the jump speed value
                _movementDirection.y = jumpSpeed;
            }
        }
        //if player is not touching the ground
        else
        {
            if (canFly)
            {
                //if the player is pressing the jump button
                if (Input.GetButton("Jump"))
                {
                    //counts how long the jump has elapsed
                    _jumpDuration += Time.deltaTime;

                    //checks if the time elapsed is equal to or over 1 second.
                    if (_jumpDuration >= 1f)
                    {
                        //invokes event to signal flying movement
                        _flyingEvent.Invoke();
                    }
                }
            }
        }

        //sets the direction of the player's downward direction to the force of gravity
        _movementDirection.y -= gravity * Time.deltaTime;

        //moves the player according to the movement direction
        _charC.Move(_movementDirection * Time.deltaTime);
    }

    private void FlyMovement()
    {
        //while in fly mode,
        //sets vertical movement force to 0
        _movementDirection.y = 0;

        //decreases the time in which the player can continue flying 
        _flightDuration -= Time.deltaTime;
        //sets the amount of seconds remaining to GUI text
        flightTimeText.text = $"Flight Time: {_flightDuration:00.0}";

        MovePlayer();

        //pressing Q will levitate player upwards, while pressing E does opposite
        if (Input.GetKey(KeyCode.Q))
        {
            _movementDirection.y += 5;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            _movementDirection.y -= 5;
        }

        //if the player is still not grounded and flight duration has exceeded it's limit
        if (!IsGrounded() && _flightDuration <= 0)
        {
            //forces the player down to the ground
            _movementDirection.y -= 20f;

            if (IsGrounded())
            {
                _landedEvent.Invoke();
            }

        }
    }

    //checks if player is grounded, returns bool value depending on result
    public bool IsGrounded()
    {
        //sends a raycast downwards from the player's position, and checks that, if directly below the player is the ground...
        if (Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.1f, groundLayer))
        {
            //then returns true because player is grounded
            return true;
        }

        //if not touching the ground, returns false
        return false;
    }
}


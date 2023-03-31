using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    float _playerHeight;

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

        _movementDirection.y -= gravity * Time.deltaTime;
        _charC.Move(_movementDirection * Time.deltaTime);
    }

<<<<<<< Updated upstream
=======
    public void FlyMovement()
    {
        //while in fly mode,
        //sets vertical movement force to 0
        _movementDirection.y = 0;

        //decreases the time in which the player can continue flying 
        _flightDuration -= Time.deltaTime;
        //sets the amount of seconds remaining to GUI text
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

        //if the player is still not grounded and flight duration has exceeded it's limit
        if (!IsGrounded() && _flightDuration <= 0)
        {
            //forces the player down to the ground
            _movementDirection.y -= 10f;
            _flightDuration = 0f;
        }
    }

    //checks if player is grounded, returns bool value depending on result
>>>>>>> Stashed changes
    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.1f, groundLayer))
        {
            return true;
        }

        return false;
    }
}


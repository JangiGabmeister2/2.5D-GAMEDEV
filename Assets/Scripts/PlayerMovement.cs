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

    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.1f, groundLayer))
        {
            return true;
        }

        return false;
    }
}


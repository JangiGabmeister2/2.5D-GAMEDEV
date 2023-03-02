using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;
    public Transform orientation;
    public Transform playerCapsule;
    public Transform player;

    [Header("Movement Speeds")]
    public float movementSpeed = 10f;
    public float jumpSpeed = 5f;
    public float gravity = 10f;
    public float rotationSpeed;

    Vector3 _movementDirection;
    Vector3 _rotationChange;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _rotationChange = new Vector3(0, 90, 0);
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            float horizontaInput = Input.GetAxis("Horizontal");

            _movementDirection = transform.TransformDirection(new Vector3(horizontaInput, 0, 0)) * movementSpeed;

            if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
            {
                player.Rotate(_rotationChange);
            }
            else if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)
            {
                player.Rotate(-_rotationChange);
            }

            if (Input.GetButton("Jump"))
            {
                _movementDirection.y = jumpSpeed;
            }
        }

        _movementDirection.y -= gravity * Time.deltaTime;
        controller.Move(_movementDirection * Time.deltaTime);
    }
}


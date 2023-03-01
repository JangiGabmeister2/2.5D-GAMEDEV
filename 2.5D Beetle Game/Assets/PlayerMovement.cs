using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform orientation;
    [SerializeField] Rigidbody body;

    Vector3 _moveDirection;

    float _horizontalInput;
    float _verticalInput;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
    }

    private void Update()
    {
        MoveInputs();    
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MoveInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {

    }
}

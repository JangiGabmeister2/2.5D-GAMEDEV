using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController _charC;
    Vector3 direction;
    public float speed = 8;
    public float rotateSpeed = 1000;

    private Vector3 _currentRotation;
    private Vector3 _nextRotation;
    private Vector3 _prevRotation;

    private Vector3 _rotationChange;

    private void Start()
    {
        _rotationChange = new Vector3(0, 90, 0);

        _currentRotation = transform.eulerAngles;
        _nextRotation = _currentRotation += _rotationChange;
        _prevRotation = _currentRotation -= _rotationChange;
    }

    private void Update()
    {
        float _horizontalInput = Input.GetAxisRaw("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");

        direction.x = _horizontalInput * speed;

        _charC.Move(direction * Time.deltaTime);

        if (_verticalInput > 0)
        {
            while (_currentRotation != _nextRotation)
            {
                transform.Rotate(_currentRotation * 90 * rotateSpeed * Time.deltaTime, Space.Self);
            }
        }
        else if (_verticalInput < 0)
        {
            while (_currentRotation != _prevRotation)
            {
                transform.Rotate(_currentRotation * -90 * rotateSpeed * Time.deltaTime, Space.Self);
            }
        }
    }
}


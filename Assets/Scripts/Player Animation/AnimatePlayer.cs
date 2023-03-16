using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    Animator _playerAnimator;
    SpriteRenderer _renderer;

    int isMoving;
    int isJumping;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();

        isMoving = Animator.StringToHash("isMoving");
        isJumping = Animator.StringToHash("isJumping");
    }

    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        bool _isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        bool _isJumping = Input.GetButton("Jump");

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _renderer.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _renderer.flipX = false;
        }   

        _playerAnimator.SetBool(isMoving, _isMoving);
        _playerAnimator.SetBool(isJumping, _isJumping);
    }
}

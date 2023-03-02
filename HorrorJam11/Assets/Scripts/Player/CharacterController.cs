using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;


    private Vector2 _movement;
    private PlayerSettings _playerSettings;

    public AudioSource StepsAudio;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerSettings = FindObjectOfType<GameManager>().PlayerSettings;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }



    void FixedUpdate()
    {
        SetMovement();
        SetAnimation();
        SetSound();
    }


    public void ReceiveWalkInput(bool performed, Vector2 movementValues)
    {
        _movement = movementValues;
    }


    public void SetAnimation()
    {
        if (_movement.x > 0)
        {
            AnimatorSetAllBoolFalseButThis(Animator.StringToHash("Side"));
            _spriteRenderer.flipX = false;
        }

        if (_movement.x < 0)
        {
            AnimatorSetAllBoolFalseButThis(Animator.StringToHash("Side"));
            _spriteRenderer.flipX = true;
        }

        if (_movement.y > 0)
        {
            AnimatorSetAllBoolFalseButThis(Animator.StringToHash("Up"));
            _spriteRenderer.flipX = false;
        }

        if (_movement.y < 0)
        {
            AnimatorSetAllBoolFalseButThis(Animator.StringToHash("Down"));
            _spriteRenderer.flipX = false;
        }

        if (_movement.magnitude == 0)
        {
            AnimatorSetAllBoolFalseButThis(Animator.StringToHash("Idle"));
            _spriteRenderer.flipX = false;
        }
    }

    public void AnimatorSetAllBoolFalseButThis(int index)  //Poderia ser um extension method
    {
        foreach (AnimatorControllerParameter boolParam in _animator.parameters)
        {
            if (boolParam.type == AnimatorControllerParameterType.Bool)
            {
                _animator.SetBool(boolParam.name, false);
            }
        }

        _animator.SetBool(index, true);
    }

    private void SetSound()
    {
        if (_movement.magnitude > 0)
        {
            if (!StepsAudio.isPlaying)
            {
                StepsAudio.Play();
            }
        }
        else
        {
            StepsAudio.Stop();
        }
    }

    private void SetMovement()
    {
        if (_movement.x != 0 & _movement.y != 0)
        {
            _movement.x = 0;
        }

        _rigidbody2D.MovePosition(_rigidbody2D.position + _movement * _playerSettings.WalkSpeed * Time.fixedDeltaTime);  //Usar o position do rigidbody2D, pois ele é vector2

    }

}

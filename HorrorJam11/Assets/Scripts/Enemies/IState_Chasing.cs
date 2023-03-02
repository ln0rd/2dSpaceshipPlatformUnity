using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IState_Chasing : IState
{
    private readonly Enemy _enemy;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private PlayerSettings _playerSettings;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;


    public IState_Chasing(Enemy enemy, Animator animator, Rigidbody2D rigidbody2D, PlayerSettings playerSettings, SpriteRenderer spriteRenderer, AudioSource audioSource)
    {
        _enemy = enemy;
        _animator = animator;
        _rigidbody2D = rigidbody2D;
        _playerSettings = playerSettings;
        _spriteRenderer = spriteRenderer;
        _audioSource = audioSource;
    }

    public void OnEnter()
    {
        _audioSource.Play();
    }

    public void OnExit()
    {
        _animator.ResetBoolParam();
        _spriteRenderer.flipX = false;
        _audioSource.AudioFadeOut(0.5f);
    }

    public void Tick()
    {
        Vector3 movDirection = (_enemy.GetPlayerPosition() - _rigidbody2D.transform.position).normalized;
        _rigidbody2D.MovePosition(_rigidbody2D.transform.position + movDirection * Time.deltaTime * _playerSettings.EnemySpeedChase);

        if (movDirection.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }

        if (movDirection.x != 0)
        {
            _animator.ResetBoolParamAndSetOneTrue(Animator.StringToHash("walk"));

        }

        if (movDirection.y < 0 && Mathf.Abs(movDirection.x) < 0.2f)
        {
            _animator.ResetBoolParamAndSetOneTrue(Animator.StringToHash("down"));
        }

        if (movDirection.y > 0 && Mathf.Abs(movDirection.x) < 0.2f)
        {
            _animator.ResetBoolParamAndSetOneTrue(Animator.StringToHash("up"));
        }


        if (!_audioSource.isPlaying) { _audioSource.Play(); }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IState_Waiting : IState
{
    private readonly Enemy _enemy;
    private Animator _animator;
    private int animId = Animator.StringToHash("idle");
    private AudioSource _audioSource;



    public IState_Waiting(Enemy enemy, Animator animator, AudioSource audioSource)
    {
        _enemy = enemy;
        _animator = animator;
        _audioSource = audioSource;
    }   

    public void OnEnter()
    {
        _animator.ResetBoolParamAndSetOneTrue(animId);
        _audioSource.Play();

    }

    public void OnExit()
    {
        _animator.ResetBoolParam();
        _audioSource.AudioFadeOut(0.5f);

    }

    public void Tick()
    {
        if (!_audioSource.isPlaying) { _audioSource.Play(); }

    }
}

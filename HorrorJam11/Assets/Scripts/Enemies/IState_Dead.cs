using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IState_Dead : IState
{
    private readonly Enemy _enemy;
    private Animator _animator;
    private int animId = Animator.StringToHash("dead");



    public IState_Dead(Enemy enemy, Animator animator)
    {
        _enemy = enemy;
        _animator = animator;
    }

    public void OnEnter()
    {
        _animator.ResetBoolParamAndSetOneTrue(animId);
        _enemy.StopAllSounds();
    }

    public void OnExit()
    {
        _animator.ResetBoolParam();

    }

    public void Tick()
    {

    }
}

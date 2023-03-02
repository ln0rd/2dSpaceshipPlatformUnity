using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IState_OnMiniGame : IState
{
    private readonly Enemy _enemy;
    private Animator _animator;
    private int animId = Animator.StringToHash("idle");

    public IState_OnMiniGame(Enemy enemy, Animator animator)
    {
        _enemy = enemy;
        _animator = animator;
    }


    public void OnEnter()
    {
        _animator.ResetBoolParamAndSetOneTrue(animId);

    }

    public void OnExit()
    {
        _animator.ResetBoolParam();

    }

    void IState.Tick()
    {

    }
}

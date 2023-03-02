using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IState_MiniGame : IState
{
    private readonly Enemy _enemy;
    private Animator _animator;
    private int animId = Animator.StringToHash("idle");
    ScenesOnBuild _minigame;

    public IState_MiniGame(Enemy enemy, Animator animator, ScenesOnBuild minigame)
    {
        _enemy = enemy;
        _animator = animator;
        _minigame = minigame;
    }

    public void OnEnter()
    {
        _animator.ResetBoolParamAndSetOneTrue(animId);
        _enemy.LoadMiniGame();

    }

    public void OnExit()
    {
        _animator.ResetBoolParam();

    }

    public void Tick()
    {

    }
}

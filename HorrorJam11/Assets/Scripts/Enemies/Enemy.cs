using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private StateMachine _stateMachine;
    private GameObject _player;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private LayerMask _layerMask;
    private SpriteRenderer _spriteRenderer;

    private float distanceToPlayer = 99999;
    private RaycastHit2D raycast;
    private bool minigame;
    private bool dead;
    private Vector2 initialPosition;
    public bool onMiniGame;

    public PlayerSettings _playerSettings;
    public ScenesOnBuild scenesOnBuild;
    public AudioSource IdleSound;
    public AudioSource WalkSound;



    // Start is called before the first frame update
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _layerMask = LayerMask.GetMask("Default", "Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();

        //State machine initiation
        _stateMachine = new StateMachine();

        //States instanciation
        var waiting = new IState_Waiting(this, _animator, IdleSound);
        var alerted = new IState_Alerted(this, _animator, _rigidbody2D, _playerSettings, _spriteRenderer, IdleSound);
        var chasing = new IState_Chasing(this, _animator, _rigidbody2D, _playerSettings, _spriteRenderer, WalkSound);
        var miniGaming = new IState_MiniGame(this, _animator, scenesOnBuild);
        var deadEnemy = new IState_Dead(this, _animator);
        var waitingForOtherMiniGame = new IState_OnMiniGame(this, _animator);

        //Add Transitions
        _stateMachine.AddTransition(waiting, alerted, PlayerInRange());
        _stateMachine.AddTransition(alerted, chasing, PlayerNotObscuredAndNear());
        _stateMachine.AddTransition(chasing, alerted, PlayerInRange());
        _stateMachine.AddTransition(alerted, waiting, PlayerNotInRange());
        _stateMachine.AddTransition(chasing, miniGaming, IsOnMiniGame());
        _stateMachine.AddTransition(miniGaming, deadEnemy, Dead());

        _stateMachine.AddTransition(waitingForOtherMiniGame, waiting, PlayerNotInRange());
        _stateMachine.AddTransition(waitingForOtherMiniGame, alerted, PlayerInRange());
        _stateMachine.AddTransition(waitingForOtherMiniGame, chasing, PlayerNotObscuredAndNear());
        _stateMachine.AddTransition(waitingForOtherMiniGame, deadEnemy, Dead());



        _stateMachine.AddAnyTransition(waitingForOtherMiniGame, OtherMiniGamePlaying());

        //Context to control trasitions
        Func<bool> PlayerNotInRange() => () => distanceToPlayer > _playerSettings.MaxRangePlayerDetection && !dead;                                
        Func<bool> PlayerInRange() => () => distanceToPlayer < _playerSettings.MaxRangePlayerDetection && distanceToPlayer > _playerSettings.ChaseTheresholdRangePlayerDetection && !dead;                                   
        Func<bool> PlayerNotObscuredAndNear() => () => raycast.collider != null && raycast.collider.gameObject.layer == 6 && !dead;
        Func<bool> Dead() => () => dead;
        Func<bool> IsOnMiniGame() => () => distanceToPlayer < 2 && !dead;
        Func<bool> OtherMiniGamePlaying() => () => onMiniGame && !dead; ;

        //Initiazation of state machine
        _stateMachine.SetState(waiting);
    }

    private void Start()
    {
        _player = FindObjectOfType<CharacterController>().gameObject;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Detect Player
        distanceToPlayer = (transform.position - _player.transform.position).magnitude;
        raycast = Physics2D.Raycast(transform.position, (_player.transform.position - transform.position), _playerSettings.ChaseTheresholdRangePlayerDetection, _layerMask);



        _stateMachine.Tick();
    }

    public void SetMiniGame(bool setTo)
    {
        minigame = setTo;
    }

    public void SetDeath()
    {
        dead = true;
    }
    
    public Vector3 GetPlayerPosition()
    {
        return _player.transform.position;
    }

    public void LoadMiniGame()
    {
        FindObjectOfType<MiniGamesManager>().StartMiniGame(scenesOnBuild, this);
    }

    public void StopAllSounds()
    {
        IdleSound.Stop();
        WalkSound.Stop(); ;
    }
}

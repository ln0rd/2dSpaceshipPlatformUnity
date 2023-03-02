using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class InteractableLogFinal : MonoBehaviour, IInteractable
{
    private LogManager _logManager;
    private bool _BeenRead = false;
    private MusicManager _musicManager;
    private Animator _playerAnimator;

    public string TextToShowOnPrompt;
    public LogsFromShip log;
    public GameObject Enemy;
    public GameObject DeadSon;
    public GameObject LockedDoor;
    public GameObject OpenDoor;

    public static event Action ReadMe;


    private void Start()
    {
        _logManager = FindObjectOfType<LogManager>();
        _musicManager = FindObjectOfType<MusicManager>();
        _playerAnimator = FindObjectOfType<CharacterController>().GetComponent<Animator>();
    }

    public void Interact()
    {
        _logManager.StartLog(log);
        if (!_BeenRead)
        {
            _BeenRead = true;
            ReadMe?.Invoke();
        }

        Enemy.SetActive(false);
        DeadSon.SetActive(true);
        LockedDoor.SetActive(false);
        OpenDoor.SetActive(true);
        _musicManager.StopMusic();
        _playerAnimator.SetLayerWeight(_playerAnimator.GetLayerIndex("Base Layer"), 0f);
        _playerAnimator.SetLayerWeight(_playerAnimator.GetLayerIndex("Enemy Layer"), 1f);
    }

    string IInteractable.PassTextToShow()
    {
        return TextToShowOnPrompt;
    }

}

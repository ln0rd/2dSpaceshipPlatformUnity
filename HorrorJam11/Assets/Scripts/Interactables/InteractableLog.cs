using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class InteractableLog : MonoBehaviour, IInteractable
{
    private LogManager _logManager;
    private bool _BeenRead = false;


    public string TextToShowOnPrompt;
    public LogsFromShip log;

    public static event Action ReadMe;


    private void Start()
    {
        _logManager = FindObjectOfType<LogManager>();
    }

    public void Interact()
    {
        _logManager.StartLog(log);
        if (!_BeenRead)
        {
            _BeenRead = true;
            ReadMe?.Invoke();
        }
    }

    string IInteractable.PassTextToShow()
    {
        return TextToShowOnPrompt;
    }

}

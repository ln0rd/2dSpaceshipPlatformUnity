using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.InputSystem.LowLevel;

public class UIInputManager : MonoBehaviour
{
    private InputActions_gameManager _gameManagerInputActions;
    private GameManager _gameManager;
    

    public static event Action TriggerDebugAction;
    public static event Action SwitchOptions;
    public static event Action ConfirmSelection;

    private void Awake()
    {
        _gameManagerInputActions = new InputActions_gameManager();
        _gameManager = GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        _gameManagerInputActions.Disable();
        _gameManagerInputActions.MainMenu.Enable();
        _gameManagerInputActions.Cutscenes.Disable();
        _gameManagerInputActions.Debug.Enable();
        

        _gameManagerInputActions.MainMenu.StartGame.performed += TryStartGamePlay;
        _gameManagerInputActions.Debug.ActivateDebugAction.performed += ActivateDebugAction;
        _gameManagerInputActions.FinalChoice.Choose.performed += FlipDecision;
        _gameManagerInputActions.FinalChoice.Confirm.performed += Confirm;
        _gameManagerInputActions.Cutscenes.Skip.performed += TrySkipCutscene;
        
        GameManager.StateChangedOrSceneLoaded += EvaluateGameState;
    }

    private void OnDisable()
    {
        _gameManagerInputActions.MainMenu.StartGame.performed -= TryStartGamePlay;
        _gameManagerInputActions.Debug.ActivateDebugAction.performed -= ActivateDebugAction;
        _gameManagerInputActions.FinalChoice.Choose.performed -= FlipDecision;
        _gameManagerInputActions.FinalChoice.Confirm.performed -= Confirm;
        _gameManagerInputActions.Cutscenes.Skip.performed -= TrySkipCutscene;

        GameManager.StateChangedOrSceneLoaded -= EvaluateGameState;

    }


    private void TryStartGamePlay(InputAction.CallbackContext context)
    {
        _gameManager.StartContext();
        //_gameManager.StartSadness(); // <-- testes
    }

    private void EvaluateGameState(GameStates currentState)
    {

        switch (currentState)
        {
            case GameStates.Menu:
                _gameManagerInputActions.Disable();
                _gameManagerInputActions.MainMenu.Enable();
                _gameManagerInputActions.Debug.Enable();
                break;
            case GameStates.CutScene:
                _gameManagerInputActions.Disable();
                _gameManagerInputActions.Cutscenes.Enable();
                break;
            case GameStates.GamePlay:
                _gameManagerInputActions.Disable();
                _gameManagerInputActions.Debug.Enable();
                break;
            case GameStates.ChoiceMenu:
                _gameManagerInputActions.Disable();
                _gameManagerInputActions.FinalChoice.Enable();
                break;
            case GameStates.MiniGame:
                _gameManagerInputActions.Disable();
                break;
            default:
                break;
        }
    }


    private void ActivateDebugAction(InputAction.CallbackContext context)
    {
        TriggerDebugAction?.Invoke();
        print("Debug action triggered!!!");
    }

    public void ActivateDebugMap()
    {
        _gameManagerInputActions.Debug.Enable();
    }

    public void FlipDecision(InputAction.CallbackContext context)
    {
        SwitchOptions?.Invoke();
    }

    public void Confirm(InputAction.CallbackContext context)
    {
        ConfirmSelection?.Invoke();
        print("Activated E key in UI Manager Map");

    }

    public void TrySkipCutscene(InputAction.CallbackContext context)
    {
        _gameManager.SkipCutscene();
    }


}

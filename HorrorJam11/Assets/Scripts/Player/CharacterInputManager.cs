using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(CharacterController), typeof(Interactor))]
public class CharacterInputManager : MonoBehaviour
{
    private InputActions_player _playerInputActions;
    private CharacterController _characterController;
    private Interactor _interactor;

    
    

    private void Awake()
    {
        _playerInputActions = new InputActions_player();
        _characterController = GetComponent<CharacterController>();
        _interactor = GetComponent<Interactor>();
    }

    private void OnEnable()
    {
        _playerInputActions.Disable();


        //Subscrever aos eventos do mapa de inputs
        _playerInputActions.CharacterMovement.Walk.performed += TryWalk;
        _playerInputActions.CharacterMovement.Walk.canceled += TryWalk;
        _playerInputActions.Interaction.Interact.performed += TryInteraction;
        


        //Subscrever aos eventos de outros compoentes
        DialogueManager.DialogueStarted += DeactivateMovement;
        DialogueManager.DialogueEnded += ActivateMovement;
        LogManager.LogStarted += DeactivateMovement;
        LogManager.LogEnded += ActivateMovement;
        GameManager.StateChangedOrSceneLoaded += EvaluateGameState;
    }

    private void OnDisable()
    {
        //Dessubescrever dos eventos do mapa de inputs para limpar os delegates
        _playerInputActions.CharacterMovement.Walk.performed -= TryWalk;
        _playerInputActions.CharacterMovement.Walk.canceled -= TryWalk;
        _playerInputActions.Interaction.Interact.performed -= TryInteraction;
        

        //Subscrever aos eventos de outros compoentes
        DialogueManager.DialogueStarted -= DeactivateMovement;
        DialogueManager.DialogueEnded -= ActivateMovement;
        LogManager.LogStarted -= DeactivateMovement;
        LogManager.LogEnded -= ActivateMovement;
        GameManager.StateChangedOrSceneLoaded -= EvaluateGameState;
    }


    private void TryWalk(InputAction.CallbackContext context)
    {
        _characterController.ReceiveWalkInput(context.performed, context.ReadValue<Vector2>());
    }

    private void TryInteraction(InputAction.CallbackContext context)
    {
        _interactor.CallInteraction();
    }




    private void DeactivateMovement()   //Necessário pois não dá para linkar o método direto na subscrição do evento.
    {
        _playerInputActions.CharacterMovement.Disable();

    }

    private void ActivateMovement()     //Necessário pois não dá para linkar o método direto na subscrição do evento.
    {
        _playerInputActions.CharacterMovement.Enable();  
    }

    private void ActivateInteraction()
    {
        _playerInputActions.Interaction.Enable();
    }

    private void DeactivateInteraction()
    {
        _playerInputActions.Interaction.Disable();
    }



    private void EvaluateGameState(GameStates currentState)
    {
        switch (currentState)
        {
            case GameStates.Menu:
                _playerInputActions.Disable();
                break;
            case GameStates.CutScene:
                _playerInputActions.Disable();
                break;
            case GameStates.GamePlay:
                _playerInputActions.Disable();
                ActivateInteraction();
                ActivateMovement();
                break;
            case GameStates.ChoiceMenu:
                _playerInputActions.Disable();
                break;
            case GameStates.MiniGame:
                _playerInputActions.Disable();
                break;
            default:
                break;
        }

    }


}

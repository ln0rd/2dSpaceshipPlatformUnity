using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractableFinalChoice : MonoBehaviour, IInteractable
{
    private GameManager _gameManager;
    public Collider2D _BacktrackHintCollider;
    public string TextToShowOnPrompt;
    public Canvas UIChoiceMenu;


    private void OnEnable()
    {
        InteractorUI.FinalChoiceMade += DisableCanvas;
    }

    private void OnDisable()
    {
        InteractorUI.FinalChoiceMade -= DisableCanvas;

    }

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        UIChoiceMenu.enabled = false;
    }



    void IInteractable.Interact()
    {
        _gameManager.ChangeGameState(GameStates.ChoiceMenu);
        _BacktrackHintCollider.enabled = true;
        UIChoiceMenu.enabled = true;
    }

    string IInteractable.PassTextToShow()
    {
        return TextToShowOnPrompt;
    }

    private void DisableCanvas()
    {
        UIChoiceMenu.enabled = false;
    }
}

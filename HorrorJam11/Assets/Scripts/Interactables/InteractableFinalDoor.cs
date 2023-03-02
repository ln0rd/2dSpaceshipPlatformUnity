using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFinalDoor : MonoBehaviour, IInteractable
{
    public string TextToShowOnPrompt;
    public Dialogue doorDialogue;

    private DialogueManager _dialogueManager;
    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }


    void IInteractable.Interact()
    {
        _dialogueManager.StartDialogue(doorDialogue);
        StartCoroutine(_gameManager.TimerForNextScene(_gameManager.StartSadness, 7f));
    }

    string IInteractable.PassTextToShow()
    {
        return TextToShowOnPrompt;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCharacter : MonoBehaviour, IInteractable
{
    private DialogueManager _dialogueManager;
    public string TextToShowOnPrompt;
    public Dialogue characterDialogue;
    private bool _BeenTalkedTo = false;


    public static event Action TalkedToMe;

    // Start is called before the first frame update
    void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
    }


    public void Interact()
    {
        _dialogueManager.StartDialogue(characterDialogue);
        if(!_BeenTalkedTo)
        {
            _BeenTalkedTo = true;
            TalkedToMe?.Invoke();
        }
    }

    string IInteractable.PassTextToShow()
    {
        return TextToShowOnPrompt;
    }
}

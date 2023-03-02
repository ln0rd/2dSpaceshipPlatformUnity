using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueByScript : MonoBehaviour
{
    private DialogueManager _dialogueManager;
    private bool _alreadyTriggered = false;

    public Dialogue popupDialogue;


    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();

    }

    public void CallDialogue()
    {
        if (!_alreadyTriggered)
        {
            _alreadyTriggered = true;
            _dialogueManager.StartDialogue(popupDialogue);
        }

    }

    
}

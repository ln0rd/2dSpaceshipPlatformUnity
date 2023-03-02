using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueByTrigger : MonoBehaviour
{
    private DialogueManager _dialogueManager;
    public bool _alreadyTriggered = false;

    public Dialogue popupDialogue;


    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_alreadyTriggered && collision.gameObject.layer == 6)
        {
            _alreadyTriggered = true;
            _dialogueManager.StartDialogue(popupDialogue);
        }

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLastInteractor : MonoBehaviour, IInteractable
{
    private DialogueManager _dialogueManager;
    private GameManager _gameManager;
    public Dialogue dialogue;
    public string TextToShowOnPrompt;
    public float delayForDialogue;
    public float delayForNextScene;

    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }


    void IInteractable.Interact()
    {
        StartCoroutine(DelayForDialogue());
    }

    string IInteractable.PassTextToShow()
    {
        return TextToShowOnPrompt;
    }

    private IEnumerator DelayForDialogue()
    {
        yield return new WaitForSeconds(delayForDialogue);
        _dialogueManager.StartDialogue(dialogue);
        StartCoroutine(DelayToLoadNextScene());
    }

    private IEnumerator DelayToLoadNextScene()
    {
        yield return new WaitForSeconds(delayForNextScene);

        if (_gameManager.GetIfGoodEnd())
        {
            StartCoroutine(_gameManager.TimerForNextScene(_gameManager.StartTheGoodEnd, 1f));
        }
        else
        {
            StartCoroutine(_gameManager.TimerForNextScene(_gameManager.StartTheBadEnd, 1f));
        }
    }

}

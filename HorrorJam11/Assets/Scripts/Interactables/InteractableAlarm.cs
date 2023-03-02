using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAlarm : MonoBehaviour, IInteractable
{
    private DialogueManager _dialogueManager;
    private AudioSource _audioSource;
    private GameManager _gameManager;
    public Dialogue dialogue;
    public string TextToShowOnPrompt;
    public float delayForDialogue;
    public float delayForNextScene;

    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = FindObjectOfType<GameManager>();
    }


    void IInteractable.Interact()
    {
        _audioSource.Play();
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
        StartCoroutine(DelayToLoadMainGame());
    }

    private IEnumerator DelayToLoadMainGame()
    {
        yield return new WaitForSeconds(delayForNextScene);
        StartCoroutine(_gameManager.TimerForNextScene(_gameManager.StartMainGame, 1f));
    }

}

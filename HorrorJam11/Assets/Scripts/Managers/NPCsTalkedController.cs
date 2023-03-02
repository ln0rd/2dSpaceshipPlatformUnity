using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsTalkedController : MonoBehaviour
{
    private int NPCsOnScene;
    private int NPCsTalked = 0;
    private GameManager _gameManager;
    private DialogueByScript _dialogueByScript;
    public DialogueByTrigger tip;
    public Collider2D lastCharacterBlocker;

    // Start is called before the first frame update
    void Start()
    {
        NPCsOnScene = FindObjectsOfType<InteractableCharacter>().Length;
        _gameManager = FindObjectOfType<GameManager>();
        _dialogueByScript = GetComponent<DialogueByScript>();
    }

    private void OnEnable()
    {
        InteractableCharacter.TalkedToMe += UpdateTalkedNPCs;
    }

    private void OnDisable()
    {
        InteractableCharacter.TalkedToMe -= UpdateTalkedNPCs;

    }

    private void UpdateTalkedNPCs()
    {
        NPCsTalked++;

        if (NPCsTalked == NPCsOnScene - 1)
        {
            StartCoroutine(WaitToGitHint());

        }

        if (NPCsTalked == NPCsOnScene)
        {
            StartCoroutine(_gameManager.TimerForNextScene(_gameManager.StartMonstersInShip, 5f));
            tip._alreadyTriggered = true;
        }
    }

    private IEnumerator WaitToGitHint()
    {
        yield return new WaitForSeconds(30f);
        _dialogueByScript.CallDialogue();
        lastCharacterBlocker.enabled = false;
    }

}

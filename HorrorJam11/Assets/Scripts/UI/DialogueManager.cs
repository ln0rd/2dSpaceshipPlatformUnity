using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class DialogueManager : MonoBehaviour
{
    [HideInInspector] public static DialogueManager Instance { get; set; }
    private int index;
    private WaitForSeconds _waitForNextChar;
    private WaitForSeconds _waitForNextLine;
    private WaitForSeconds _waitForDisable;
    private bool OnGoingDialogue;
    private bool firstTimeDialogue = true;
    public AudioSource _openSound;
    public AudioSource _textSound;

    public TMP_Text dialogBox;
    public Image avatarBox;
    public TMP_Text nameBox;
    public float textSpeed;
    public float lineSpeed;
    public float disablePeriod;
    public Sprite playerAvatar;
    public GameObject TextElements;
    public GameObject AvatarElements;

    public static event Action DialogueStarted;
    public static event Action DialogueEnded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        _waitForNextChar = new WaitForSeconds(textSpeed);
        _waitForNextLine = new WaitForSeconds(lineSpeed);
        _waitForDisable = new WaitForSeconds(disablePeriod);
        dialogBox.text = string.Empty;

        TextElements.SetActive(false);
        AvatarElements.SetActive(false);

    }



    public void StartDialogue(Dialogue dialogue)
    {
        if (!OnGoingDialogue)
        {
            OnGoingDialogue = true;
            DialogueStarted?.Invoke();

            index = 0;
            StartCoroutine(ProgressDialogue(dialogue));
            _openSound.Play();
            //if (firstTimeDialogue)  //Esqueleto para mostrar somente a última fala depois
            //{
            //}
            //else
            //{
            //    index = dialogue.Dialogues.Length - 1;
            //    ShowLastLineOnly(dialogue);
            //}


        }
    }

    private IEnumerator ProgressDialogue(Dialogue dialogue)
    {
        ActivateElements();

        foreach (string line in dialogue.Dialogues)
        {
            SetAvatarForCurrentLine(dialogue);

            foreach (char c in dialogue.Dialogues[index].ToCharArray())
            {
                if (!_textSound.isPlaying) { _textSound.Play(); }
                dialogBox.text += c;
                yield return _waitForNextChar;
            }
            _textSound.Stop();
            yield return _waitForNextLine;
            index++;

            if (index != dialogue.Dialogues.Length)
            {
                dialogBox.text = string.Empty;
            }

        }

        yield return _waitForDisable;

        EndDialogue();
    }

    private void ActivateElements()
    {
        TextElements.SetActive(true);
        AvatarElements.SetActive(true);

    }

    private void SetAvatarForCurrentLine(Dialogue dialogue)
    {
        if (dialogue.NPCLine[index])
        {
            avatarBox.sprite = dialogue.NPCAvatar;
            nameBox.text = dialogue.NPCName;
        }
        else
        {
            avatarBox.sprite = playerAvatar;
            nameBox.text = "You";
        }
    }


    private void EndDialogue()
    {
        dialogBox.text = string.Empty;
        nameBox.text = string.Empty;
        avatarBox.sprite = null;

        TextElements.SetActive(false);
        AvatarElements.SetActive(false);

        OnGoingDialogue = false;
        firstTimeDialogue = false;

        DialogueEnded?.Invoke();
        _textSound.Stop();
        StopAllCoroutines();
    }

    private void ShowLastLineOnly(Dialogue dialogue)
    {
        Debug.Log("x");
        ActivateElements();
        SetAvatarForCurrentLine(dialogue);
        dialogBox.text = dialogue.Dialogues[index];
        EndDialogue();
    }
}

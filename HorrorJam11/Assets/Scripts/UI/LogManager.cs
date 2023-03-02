using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class LogManager : MonoBehaviour
{
    [HideInInspector] public static LogManager Instance { get; set; }
    private int index;
    private WaitForSeconds _waitForNextChar;
    private WaitForSeconds _waitForNextLine;
    private WaitForSeconds _waitForDisable;
    private bool OnGoingLog;
    private bool firstTimeDialogue = true;
    public AudioSource _logAudio;
    public AudioSource _textSound;

    public TMP_Text dialogBox;
    public float textSpeed;
    public float lineSpeed;
    public float disablePeriod;
    public GameObject TextElements;

    public static event Action LogStarted;
    public static event Action LogEnded;

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

    }



    public void StartLog(LogsFromShip log)
    {
        if (!OnGoingLog)
        {
            OnGoingLog = true;
            LogStarted?.Invoke();

            index = 0;
            StartCoroutine(ProgressDialogue(log));
            _logAudio.Play();

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

    private IEnumerator ProgressDialogue(LogsFromShip log)
    {
        ActivateElements();
        foreach (string line in log.logContent)
        {

            foreach (char c in log.logContent[index].ToCharArray())
            {
                if (!_textSound.isPlaying) { _textSound.Play(); }
                dialogBox.text += c;
                yield return _waitForNextChar;
            }
            _textSound.Stop();
            yield return _waitForNextLine;
            index++;

            if (index != log.logContent.Length)
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

    }




    private void EndDialogue()
    {
        dialogBox.text = string.Empty;

        TextElements.SetActive(false);

        OnGoingLog = false;
        firstTimeDialogue = false;

        LogEnded?.Invoke();
        _textSound.Stop();
        StopAllCoroutines();
    }

    private void ShowLastLineOnly(LogsFromShip log)
    {
        ActivateElements();
        dialogBox.text = log.logContent[index];
        EndDialogue();
    }
}

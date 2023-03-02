using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class MiniGameManager : MonoBehaviour
{
    [Header("Setup")]
    public Image progressBar;
    public TMP_Text HitText;

    [Header("Difficult setup")]
    public int initialHits;
    public float duration;


    private int currentHits;
    private float currentDuration;
    private bool notifiedEndGame;

    public static event Action Victory;
    public static event Action Loss;


    private void OnEnable()
    {
        Bullet1.HitPlayer += SubtractHit;
    }

    private void OnDisable()
    {
        Bullet1.HitPlayer -= SubtractHit;

    }
    // Start is called before the first frame update
    void Start()
    {
        currentHits = initialHits;
        currentDuration = 0;

        progressBar.fillAmount = 0;
        HitText.text = "Hits x " + currentHits;
    }

    // Update is called once per frame
    void Update()
    {
        //Duration control
        currentDuration += Time.deltaTime;
        progressBar.fillAmount = currentDuration / duration;
        if (currentDuration >= duration && !notifiedEndGame)
        {
            Victory?.Invoke();
            notifiedEndGame = true;
        }

        //Hits control
        if (currentHits < 0 && !notifiedEndGame)
        {
            Loss?.Invoke();
            notifiedEndGame = true;
        }
    }

    private void SubtractHit()
    {
        currentHits--;
        if (currentHits >= 0)
        {
            HitText.text = "Hits x " + currentHits;
        }
    }
}

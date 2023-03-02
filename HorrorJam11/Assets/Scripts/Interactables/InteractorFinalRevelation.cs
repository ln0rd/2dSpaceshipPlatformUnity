using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorFinalRevelation : MonoBehaviour, IInteractable
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public string PassTextToShow()
    {
        throw new System.NotImplementedException();
    }
}

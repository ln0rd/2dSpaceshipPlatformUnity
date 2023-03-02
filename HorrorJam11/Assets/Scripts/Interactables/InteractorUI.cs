using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractorUI : MonoBehaviour
{
    //O certo seria implantar uma interface com os métodos CallConfirmation e CallSwitchOptions. Mas como é somente essa instância, vou fazer direto aqui.
    //Boiler plate FTW!!! 

    public Image[] choiceCursors;
    private int _index;
    private GameManager _gameManager;
    private InteractableFinalChoice _interactableFinalChoice;
    public AudioSource ConfirmationSound;
    public AudioSource OpenSound;
    public AudioSource ChangeSound;

    public static event Action FinalChoiceMade;

    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
        _gameManager = FindObjectOfType<GameManager>();
        _interactableFinalChoice = FindObjectOfType<InteractableFinalChoice>();
        OpenSound.Play();
    }

    private void OnEnable()
    {
        UIInputManager.SwitchOptions += SwitchOptions;
        UIInputManager.ConfirmSelection += CallConfirmation;
    }

    private void OnDisable()
    {
        UIInputManager.SwitchOptions -= SwitchOptions;
        UIInputManager.ConfirmSelection -= CallConfirmation;
    }


    public void CallConfirmation()
    {
        switch (_index)
        {
            case 0:
                _gameManager.SetEnding(true);
                break;
            case 1:
                _gameManager.SetEnding(false);
                break;
            default:
                //Default: index = 0;
                _gameManager.SetEnding(true);
                break;
        }

        ConfirmationSound.Play();
        _gameManager.ChangeGameState(GameStates.GamePlay);
        _interactableFinalChoice.UIChoiceMenu.enabled = false;
        FinalChoiceMade?.Invoke();
        _interactableFinalChoice.gameObject.SetActive(false);
    }

    public void SwitchOptions()
    {
        ChangeIndex();
        foreach (Image cursor in choiceCursors)
        {
            cursor.enabled = false;
        }

        choiceCursors[_index].enabled = true;
        ChangeSound.Play();
    }

    private void ChangeIndex()
    {
        _index++;
        if (_index > choiceCursors.Length - 1)
        {
            _index = 0;
        }
    }

}

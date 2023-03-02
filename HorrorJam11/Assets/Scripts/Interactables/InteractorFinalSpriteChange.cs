using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorFinalSpriteChange : MonoBehaviour, IInteractable
{
    public GameObject Enemy;
    public GameObject DeadSon;
    // Colocar a porta aqui, e fazer ela abrir.

    void IInteractable.Interact()
    {
        print("x");
        Vector3 pos = Enemy.transform.position;
        Enemy.SetActive(false);
        DeadSon.SetActive(true);
        DeadSon.transform.position = pos;
        //Colocar abertura da porta
    }

    string IInteractable.PassTextToShow()
    {
        return "";
    }


}

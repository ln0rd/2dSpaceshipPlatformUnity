using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(Collider2D))]
public class Interactor : MonoBehaviour
{
    public Collider2D triggerForInteraction;
    public GameObject promptToInteract;
    public Transform spawnPoint;

    private GameObject prompt;
    private GameObject objectToInteract;

    private void Start()
    {
        if (!triggerForInteraction.isTrigger)
        {
            Debug.Log("ATENÇÃO: Alterado collider para trigger");
            triggerForInteraction.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player Interactables"))
        {

            prompt = Instantiate(promptToInteract, spawnPoint.position, Quaternion.identity, transform);
            objectToInteract = collision.gameObject;
            prompt.GetComponentInChildren<TMP_Text>().text = "[" + objectToInteract.GetComponent<IInteractable>().PassTextToShow() + "]";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player Interactables") && prompt != null)
        {
            prompt.GetComponentInChildren<TMP_Text>().text = string.Empty;
            Destroy(prompt);
            objectToInteract = null;
        }
    }

    public void CallInteraction()
    {
        if (objectToInteract != null)
        {
            objectToInteract.GetComponent<IInteractable>().Interact();
            Destroy(prompt, 0.5f);
            objectToInteract = null;  //Testar 
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLockedDoor : MonoBehaviour, IInteractable
{
    private Collider2D _collider;
    private AudioSource _audioSource;

    public string TextToShowOnPrompt;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    void IInteractable.Interact()
    {

    }

    string IInteractable.PassTextToShow()
    {
        return TextToShowOnPrompt;
    }
}

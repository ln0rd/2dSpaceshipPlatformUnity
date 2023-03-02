using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour, IInteractable
{
    private Collider2D _collider;
    private AudioSource _audioSource;

    public Transform leftDoor;
    public Transform rightDoor;
    public string TextToShowOnPrompt;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    void IInteractable.Interact()
    {
        _collider.enabled = false;

        leftDoor.GetComponent<Animator>().SetBool("open", true);
        rightDoor.GetComponent<Animator>().SetBool("open", true);
        _audioSource.Play();
    }

    string IInteractable.PassTextToShow()
    {
        return TextToShowOnPrompt;
    }
}

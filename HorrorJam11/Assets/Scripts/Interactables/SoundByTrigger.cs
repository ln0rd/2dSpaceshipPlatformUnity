using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundByTrigger : MonoBehaviour
{
    private AudioSource _audioSource;
    private bool _alreadyTriggered = false;
    public bool FadeAfterSeconds;
    public float MaxDuration;
    private WaitForSeconds waitToFade;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        waitToFade = new WaitForSeconds(MaxDuration);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_alreadyTriggered && collision.gameObject.layer == 6)
        {
            _alreadyTriggered = true;
            _audioSource.Play();
            if (FadeAfterSeconds)
            {
                StartCoroutine(WaitToStartFade());
            }
        }

    }

    private IEnumerator WaitToStartFade()
    {
        yield return waitToFade;
        StartCoroutine(_audioSource.AudioFadeOut(0.5f));

    }
}

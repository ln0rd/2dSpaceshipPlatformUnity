using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMP_TextProgressive : MonoBehaviour
{
    private TMP_Text _Text;
    private string _originalText;
    private WaitForSeconds _delay;
    private AudioSource _audioSource;
    public float characterDelay;
    

    // Start is called before the first frame update
    void Start()
    {
        _Text = GetComponent<TMP_Text>();
        _originalText = _Text.text;
        _Text.text = string.Empty;
        _delay = new WaitForSeconds(characterDelay);
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(ShowTextProgressive());

    }



    private IEnumerator ShowTextProgressive()
    {
        yield return new WaitForSeconds(2f);
        _audioSource.Play();
        foreach (char c in _originalText)
        {
            _Text.text += c;
            yield return _delay;
        }
        _audioSource.Stop();
    }
}

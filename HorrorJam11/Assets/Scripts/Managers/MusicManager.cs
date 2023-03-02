using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private MusicManager Instance;
    private AudioSource musicSource;
    private float fadeTime = 1f;
    private float initialVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource = GetComponent<AudioSource>();
        initialVolume = musicSource.volume;
    }


    public void ChangeAndPlayMusicDelayed(ScenesOnBuild scene)
    {
        StopMusic();
        musicSource.clip = Resources.Load<AudioClip>(scene.ToString());
        musicSource.Play();
        //StartCoroutine(FadeIn());
    }
    //public void PlayMainMenu()
    //{

    //    StopMusic();
    //    musicSource.clip = Resources.Load<AudioClip>("Placeholder");
    //    musicSource.Play();
    //}


    //public void PlayGamePlay()
    //{
    //    StopMusic();
    //    musicSource.clip = Resources.Load<AudioClip>("Placeholder2");
    //    musicSource.Play();
    //}


    public void StopMusic()
    {

        if (musicSource.clip != null)
        {
            //StartCoroutine(FadeOut());
            musicSource.Stop();
        }

    }

    public IEnumerator FadeOut()
    {

        float timer = 0f;
        while (timer <= fadeTime)
        {

            musicSource.volume = Mathf.Clamp(musicSource.volume - musicSource.volume * 0.2f, 0.01f, 1);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {

        float timer = 0f;
        while (timer <= fadeTime)
        {

            musicSource.volume = Mathf.Clamp(musicSource.volume + musicSource.volume * 0.2f, 0.01f, initialVolume);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class MySceneManager : MonoBehaviour
{
    [HideInInspector] public static MySceneManager Instance { get; set; }
    private Animator _animator;

    //Exposed properties
    [SerializeField] private float _minimumLoadTime;
    [SerializeField] private AnimationClip _beginLoadAnimation;
    [SerializeField] private AnimationClip _endLoadAnimation;

    public static event Action SceneLoaded;

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

        _animator = GetComponent<Animator>();
        _minimumLoadTime = Mathf.Max(_minimumLoadTime, _beginLoadAnimation.length);
    }


    public void LoadScene(ScenesOnBuild sceneToLoad)
    {
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }



    private IEnumerator LoadSceneAsync(ScenesOnBuild sceneToLoad)
    {
        float timer = 0f;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad.ToString());
        asyncOperation.allowSceneActivation = false; //trava o progresso do loading em 0.9f, de acordo com documentação

        _animator.SetTrigger("start");

        while (timer <= _minimumLoadTime && asyncOperation.progress <= 0.9f)
        {
            timer += Time.deltaTime;
            //Debug.Log("Loading: " + asyncOperation.progress * 100 + "%");
            yield return null;
        }

        timer = 0f;  //Re utilizar a variável para testar se já passou um frame

        asyncOperation.allowSceneActivation = true; //Libera a ativação da cena quando acabar de carregar
        
        while(timer <= Time.deltaTime) //Garante que 1 ou 2 frames passem antes de liberar o painel de transição
        {
            timer += Time.deltaTime;
            yield return null;
        }

        _animator.SetTrigger("end");
        SceneLoaded?.Invoke();
    }

}



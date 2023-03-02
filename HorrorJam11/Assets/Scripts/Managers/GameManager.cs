using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Não expostas
    private GameManager Instance;
    [HideInInspector] public GameStates currentState { get; private set; }
    private MySceneManager _sceneManager;
    private MusicManager _musicManager;
    private bool _goodEnding;
    private bool _canSkipCutscene;
    private Enemy[] enemies;
    
    

    //Expostas
    public PlayerSettings PlayerSettings;
    

    public static event Action<GameStates> StateChangedOrSceneLoaded;

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

        _sceneManager = FindObjectOfType<MySceneManager>();
        _musicManager = FindObjectOfType<MusicManager>();
    }

    private void OnEnable()
    {
        MySceneManager.SceneLoaded += SceneLoaded;


    }

    private void OnDisable()
    {
        MySceneManager.SceneLoaded -= SceneLoaded;

    }



    private void Start()
    {
        //Não precisa chamar a cena do Menu, pois é a cena 0 da build
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.c0_MainMenu);
        ChangeGameState(GameStates.Menu);
        

    }

    public void StartOpenScene()
    {
        _sceneManager.LoadScene(ScenesOnBuild.c0_MainMenu);
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.c0_MainMenu);
        ChangeGameState(GameStates.Menu);
    }

    public void StartContext()
    {
        _sceneManager.LoadScene(ScenesOnBuild.c1_TheContext);
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.c1_TheContext);
        ChangeGameState(GameStates.CutScene);
        StartCoroutine(TimeToSkipCutScene());
        _canSkipCutscene = false;
    }

    public void StartPrelude()
    {
        _sceneManager.LoadScene(ScenesOnBuild.c2_TheCalm);
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.c2_TheCalm);
        ChangeGameState(GameStates.GamePlay);
    }

    public void StartMonstersInShip()
    {
        _sceneManager.LoadScene(ScenesOnBuild.c3_TheKitchen);
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.c3_TheKitchen);
        ChangeGameState(GameStates.GamePlay);
    }

    public void StartMainGame()
    {
        _sceneManager.LoadScene(ScenesOnBuild.c4_TheStorm);
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.c4_TheStorm);
        ChangeGameState(GameStates.GamePlay);
    }

    public void StartSadness()
    {
        _sceneManager.LoadScene(ScenesOnBuild.c5_TheSonAndTheChoise);
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.c4_TheStorm);
        ChangeGameState(GameStates.GamePlay);
    }

    public void StartRevelation()
    {
        _sceneManager.LoadScene(ScenesOnBuild.c6_TheRevelation);
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.c6_TheRevelation);
        ChangeGameState(GameStates.GamePlay);
    }

    public void StartTheBadEnd()
    {
        _sceneManager.LoadScene(ScenesOnBuild.c7_TheBadEnd);
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.c7_TheBadEnd);
        ChangeGameState(GameStates.CutScene);
        StartCoroutine(TimerForNextScene(StartOpenScene, 45f));

    }

    public void StartTheGoodEnd()
    {
        _sceneManager.LoadScene(ScenesOnBuild.c7_TheGoodEnd);
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.c7_TheGoodEnd);
        ChangeGameState(GameStates.CutScene);
        StartCoroutine(TimerForNextScene(StartOpenScene, 45f)); 
    }

    public void StartGameOver()
    {
        _sceneManager.LoadScene(ScenesOnBuild.c8_GameOver);
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.c8_GameOver);
        ChangeGameState(GameStates.CutScene);
        StartCoroutine(TimerForNextScene(StartMonstersInShip, 20f));
    }

    public void ChangeGameState(GameStates toState)
    {
        currentState = toState;
        StateChangedOrSceneLoaded?.Invoke(currentState);
    }

    public void SceneLoaded()
    {
        StateChangedOrSceneLoaded?.Invoke(currentState);
        enemies = FindObjectsOfType<Enemy>();
    }



    public IEnumerator TimerForNextScene(Action methodForNextScene, float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        methodForNextScene();
    }

    public void SetEnding(bool goodEnding)
    {
        _goodEnding = goodEnding;
    }

    public bool GetIfGoodEnd()
    {
        return _goodEnding;
    }


    private IEnumerator TimeToSkipCutScene()
    {
        yield return new WaitForSeconds(5f);
        _canSkipCutscene = true;
    }

    public void SkipCutscene()
    {
        if(_canSkipCutscene)
        {
            StartPrelude();
        }
    }

    public void SetOnEnemiesToMiniGameState(bool setTo)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.onMiniGame = setTo;
        }
    }
}



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGamesManager : MonoBehaviour
{
    private GameManager _gameManager;
    private MusicManager _musicManager;
    public GameObject MiniGamePanel;
    private GameObject currentEnemy;
    private ScenesOnBuild currentMiniGameScene;

    public static event Action MiniGameWon;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _musicManager = FindObjectOfType<MusicManager>();
        MiniGamePanel.SetActive(false);

    }

    private void OnEnable()
    {
        MiniGameManager.Victory += EndMiniGameVictory;
        MiniGameManager.Loss += EndMiniGameGameOver;
    }

    private void OnDisable()
    {
        MiniGameManager.Victory -= EndMiniGameVictory;
        MiniGameManager.Loss -= EndMiniGameGameOver;
    }



    public void StartMiniGame(ScenesOnBuild minigame, Enemy enemyToKill)  
    {
        MiniGamePanel.SetActive(true);
        SceneManager.LoadScene((int)minigame, LoadSceneMode.Additive);
        _gameManager.ChangeGameState(GameStates.MiniGame);
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.MiniGameMusic_DoNotLoadAsScene);
        currentEnemy = enemyToKill.gameObject;
        currentMiniGameScene = minigame;
        _gameManager.SetOnEnemiesToMiniGameState(true);
    }

    public void EndMiniGameVictory()
    {
        MiniGamePanel.SetActive(false);
        SceneManager.UnloadSceneAsync((int)currentMiniGameScene);
        _gameManager.ChangeGameState(GameStates.GamePlay);
        _musicManager.ChangeAndPlayMusicDelayed(ScenesOnBuild.c4_TheStorm);
        currentEnemy.GetComponent<Enemy>().SetDeath();
        _gameManager.SetOnEnemiesToMiniGameState(false);
        MiniGameWon?.Invoke();
    }

    public void EndMiniGameGameOver()
    {
        _gameManager.StartGameOver();
    }
}

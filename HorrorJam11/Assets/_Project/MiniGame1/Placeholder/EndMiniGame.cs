using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMiniGame : MonoBehaviour
{
    private MiniGamesManager miniGamesManager;
    public bool victory;

    // Start is called before the first frame update
    void Start()
    {
        miniGamesManager = FindObjectOfType<MiniGamesManager>();
        StartCoroutine(EndGame(victory));
    }


    private IEnumerator EndGame(bool victory)
    {
        yield return new WaitForSeconds(5f);
        if(victory)
        {
            miniGamesManager.EndMiniGameVictory();
        }
        else
        {
            miniGamesManager.EndMiniGameGameOver();
        }
    }
}

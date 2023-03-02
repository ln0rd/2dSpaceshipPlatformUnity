using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLastScene : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _gameManager.StartRevelation();
        }
    }
}

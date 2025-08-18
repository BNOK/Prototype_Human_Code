using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices;

public class SimpleGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameplayHUD;
    private GameplayController _controller;

    [SerializeField]
    private GameObject _GameOverHUD;
    private ScoreController _endScore;

    [SerializeField]
    private GameObject _MainMenuHUD;

    //[SerializeField]
    //private GameObject _MainMenuHUD;

    private void Start()
    {
        _controller = _gameplayHUD.GetComponent<GameplayController>();
        _endScore = _GameOverHUD.GetComponent<ScoreController>();
    }

    public void StartNewGame()
    {
        if (_GameOverHUD.activeSelf)
        {
            _GameOverHUD.SetActive(false);
        }
        if (_MainMenuHUD.activeSelf)
        {
            _MainMenuHUD.SetActive(false);
        }

        _gameplayHUD.SetActive(true);
        _controller.RestartGame();
        
    }

    public void QuitGame()
    {
        //add save logic
        Application.Quit();
    }

    public void EndGame(int points, int turns)
    {
        // handle Game Over UI 
        _GameOverHUD?.SetActive(true);
        _endScore = _GameOverHUD.GetComponent<ScoreController>();
        _endScore.UpdateValues(points, turns);

        // handle gameplay UI
        _controller.EndGame();
        _gameplayHUD.SetActive(false);

        _MainMenuHUD.SetActive(false);
    }

    #region SaveLoadSystem
    
    #endregion
}

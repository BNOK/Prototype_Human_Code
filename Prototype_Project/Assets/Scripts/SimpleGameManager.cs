using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameplayHUD;
    [SerializeField]
    private GameObject _GameOverHUD;
    [SerializeField]
    private ScoreController _endScore;

    //[SerializeField]
    //private GameObject _MainMenuHUD;

    private void Start()
    {
        //_endScore = GetComponentInChildren<ScoreController>();
    }

    public void StartNewGame()
    {
        if (_GameOverHUD.activeSelf)
        {
            _GameOverHUD.SetActive(false);
        }
        //if(_MainMenuHUD.activeSelf)
        //{
        //    _MainMenuHUD.SetActive(false);
        //}
        _gameplayHUD.SetActive(true);
        
    }

    public void QuitGame()
    {
        //add save logic
        Application.Quit();
    }

    public void EndGame(int points, int turns)
    {
        _GameOverHUD?.SetActive(true);
        _endScore.UpdateValues(points, turns);

        _gameplayHUD?.SetActive(false);
        //_MainMenuHUD.SetActive(false);
    }
}

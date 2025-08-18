using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    private SimpleGameManager _gameManager;
    private GridManager _gridManager;
    private List<Card> _cards;
    private int _disabledCards = 0;
    [SerializeField]
    private ScoreController _scoreController;

    [SerializeField]
    private int _points =0, _turns = 0;
    private bool previousTurn = false;

    private bool gameStarted = false;

    private void OnEnable()
    {
        _cards = new List<Card>();
        if(!gameStarted && _gridManager != null)
        {
            _cards = _gridManager.SetupGameGrid(this);
            gameStarted = true;
        }
    }
    private void Start()
    {
        if(_gameManager == null)
        _gameManager = GameObject.FindFirstObjectByType<SimpleGameManager>();
        if(_gridManager == null)
        _gridManager = GetComponentInChildren<GridManager>();

        if(_cards.Count == 0)
        {
            _cards = _gridManager.SetupGameGrid(this);
            gameStarted = true;
        }
        
    }

    int _selectionCounter = 0;
    List<Card> _selectedCards = new List<Card>();
    public void ClickHandle(Card clickedCard)
    {
        _selectedCards.Add(clickedCard);
        _selectionCounter++;
        if (_selectionCounter == 2)
        {
            List<Card> cardHolder = new List<Card>(_selectedCards);
            StartCoroutine(CompareCoroutine(cardHolder));
            _selectedCards.Clear();
            _selectionCounter = 0;
        }

    }

    IEnumerator CompareCoroutine(List<Card> input)
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log($"ID1 : {input[0].GetID()}, ID2: {input[1].GetID()}");
        bool result = input[0].GetID() == input[1].GetID();

        ProcessScore(result);

        yield return new WaitForSeconds(0.1f);
        if (result)
        {
            foreach (Card card in input)
            {
                card.DisableCard();
                _disabledCards++;
            }
            // scoring(+1 point, +1 turn)
        }
        else
        {
            foreach (Card card in input)
            {
                card.HideCard();
            }
            //scoring (no points, +1 turn)
        }

        yield return CheckGameState();
    }

    private void ProcessScore(bool turnResult)
    {
        if (turnResult)
        {
            // if previous turn is a match , +2 instead of +1 (COMBO ...)
            if (previousTurn)
            {
                _points++;
            }
            _points++;
        }
        _turns++;
        previousTurn = turnResult;
        _scoreController.UpdateValues(_points, _turns);
    }

    private IEnumerator CheckGameState()
    {
        if (_disabledCards >= _cards.Count)
        {
            yield return new WaitForSeconds(1.0f);
            foreach (Card card in _cards)
            {
                Destroy(card.gameObject);
            }
            _cards.Clear();

            _gameManager.EndGame(_points, _turns);

        }

        yield break;
    }

    public void EndGame()
    {
        _points = 0;
        _turns = 0;
        foreach (Card card in _cards)
        {
            Destroy(card.gameObject);
        }
        _cards.Clear();
        _disabledCards = 0;
        gameStarted = false;
    }

    public void RestartGame()
    {
        if(_scoreController != null)
        {
            _scoreController.UpdateValues(_points, _turns);
        }
    }
}

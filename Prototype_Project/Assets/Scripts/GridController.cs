using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    private List<Card> _cards;
    void Start()
    {
        Debug.Log("Start OFF");
        // getting component and setting cellSize depending on the card size
        _gridComponent = GetComponent<GridLayoutGroup>();
        
        // setting up the grid (mobile)
        SetGridLayout(2);
        CheckGridSize();
        SetCellSize();
        // setting up the cards
        List<Tuple<int, Sprite>> temp = CreateIconList();
        SpawnCardGrid(temp, _gridWidth, _gridHeight);
        
    }

    public List<Card> getCards()
    {
        return _cards;
    }

    #region GridSetup

    // Grid Properties
    [SerializeField, Range(2, 5)]
    private int _gridWidth, _gridHeight;

    [SerializeField]
    private Card _cardPrefab;

    [SerializeField]
    private Sprite[] _icons;

    private GridLayoutGroup _gridComponent;

    private void SetGridLayout(int platformID)
    {
        // 1 == DESKTOP , 2 == MOBILE
        if (platformID == 1)
        {
            _gridComponent.startAxis = GridLayoutGroup.Axis.Horizontal;
            _gridComponent.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            _gridComponent.constraintCount = 5;
        }
        else if (platformID == 2)
        {
            _gridComponent.startAxis = GridLayoutGroup.Axis.Vertical;
            _gridComponent.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridComponent.constraintCount = 5;
        }
    }

    private void SetCellSize()
    {
        // get values from the card template
        float[] cellSize = _cardPrefab.GetCardSize();
        _gridComponent.cellSize = new Vector2(cellSize[0], cellSize[1]);
        _gridComponent.spacing = new Vector2(cellSize[0] / 4, cellSize[1] / 4);
    }

    // spawn cards and assign them to a 1D array since its more efficient
    private void SpawnCardGrid(List<Tuple<int, Sprite>> iconList, int width = 2, int height = 4)
    {
        _gridHeight = height;
        _gridWidth = width;

        _cards = new List<Card>();
        for (int i = 0; i < width * height; i++)
        {
            Card tempcard = Instantiate(_cardPrefab, this.transform);
            
            tempcard.SetupCard(iconList[i].Item1, iconList[i].Item2, () => { ClickHandle(tempcard); });

            _cards.Add(tempcard);
        }
    }

    

    //change grid size to have an even number of cards
    private void CheckGridSize()
    {
        int size = _gridHeight * _gridWidth;
        if (size % 2 != 0)
        {
            if (_gridHeight % 2 != 0) _gridHeight--;
            else _gridWidth++;
        }
    }

    private List<Tuple<int, Sprite>> CreateIconList()
    {
        List<Tuple<int, Sprite>> resultList = new List<Tuple<int, Sprite>>();
        int counter = 1;
        for (int i = 0; i < _gridHeight * _gridWidth; i++)
        {
            if (i % 2 == 0) counter++;
            Tuple<int, Sprite> temp = Tuple.Create(counter, _icons[counter]);
            resultList.Add(temp);
        }

        ListShuffle(resultList);

        return resultList;
    }


    private void ListShuffle(List<Tuple<int, Sprite>> List)
    {
        int length = List.Count;
        for (int i = length - 1; i > 0; i--)
        {
            int rand = UnityEngine.Random.Range(0, i + 1);
            (List[i], List[rand]) = (List[rand], List[i]);
        }
    }

    #endregion

    #region Matching Logic

    // Click Handle Properties
    int _selectionCounter = 0;
    List<Card> _selectedCards = new List<Card>();
    public void ClickHandle(Card clickedCard)
    {
        _selectedCards.Add(clickedCard);
        _selectionCounter++;
        if(_selectionCounter == 2)
        {
            List<Card> cardHolder = new List<Card>(_selectedCards);
            StartCoroutine(CompareCoroutine(cardHolder));
            _selectedCards.Clear();
            _selectionCounter = 0;
        }
    }

    IEnumerator CompareCoroutine(List<Card> input)
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log($"ID1 : {input[0].GetID()}, ID2: {input[1].GetID()}");
        bool result = input[0].GetID() == input[1].GetID();

        yield return new WaitForSeconds(1.0f);
        if (result)
        {
            foreach (Card card in input)
            {
                card.DisableCard();
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

        yield break;
    }
    #endregion

    #region Scoring
    private int _matches, _turns;


    #endregion
}

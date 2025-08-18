using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    //private List<Card> _cards;
    private GridLayoutGroup _gridComponent;
    // Grid Properties
    [SerializeField, Range(2, 5)]
    private int _gridWidth, _gridHeight;

    [SerializeField]
    private Card _cardPrefab;

    [SerializeField]
    private Sprite[] _icons;

    public List<Card> SetupGameGrid(GameplayController gameplayController)
    {
        // getting component and setting cellSize depending on the card size
        _gridComponent = GetComponent<GridLayoutGroup>();
        // setting up the grid (mobile)
        SetGridLayout(2);
        SetCellSize();
        CheckGridSize(); // ** CRUCIAL TO FIX THE SIZE IN CASE OF LOW ICON COUNT **
        // setting up the cards


        List<Tuple<int, Sprite>> temp = CreateIconList();
        

        return SpawnCardGrid(temp, gameplayController,  _gridWidth, _gridHeight);
    }

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
    public List<Card> SpawnCardGrid(List<Tuple<int, Sprite>> iconList, GameplayController controller, int width = 2, int height = 4)
    {
        List<Card> resultCards = new List<Card>();
        _gridHeight = height;
        _gridWidth = width;

        for (int i = 0; i < width * height; i++)
        {
            Card tempcard = Instantiate(_cardPrefab, this.transform);

            tempcard.SetupCard(iconList[i].Item1, iconList[i].Item2, () => { controller.ClickHandle(tempcard); });

            resultCards.Add(tempcard);
        }

        return resultCards;
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
}

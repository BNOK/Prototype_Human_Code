using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{

    [SerializeField, Range(2, 5)]
    private int _gridWidth, _gridHeight;

    [SerializeField]
    private Card _cardPrefab;

    [SerializeField]
    private Sprite[] _icons;

    private List<Card> _cards;

    private GridLayoutGroup _gridComponent;


    
    void Start()
    {
        // getting component and setting cellSize depending on the card size
        _gridComponent = GetComponent<GridLayoutGroup>();
        // setting up the grid
        SetGridLayout(2);
        CheckGridSize();
        SetCellSize();
        // setting up the cards
        List<Sprite> temp = CreateIconList();
        SpawnCardGrid(_gridWidth,_gridHeight);
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
    private void SpawnCardGrid(int width = 2, int height = 4)
    {
        _gridHeight = height;
        _gridWidth = width;

        _cards = new List<Card>();
        for(int i=0; i< width * height; i++)
        {
            Card tempcard = Instantiate(_cardPrefab, this.transform);
            //tempcard.SetCardIcon();

            _cards.Add(tempcard);
        }
    }

    //change grid size to have an even number of cards
    private void CheckGridSize()
    {
        int size = _gridHeight * _gridWidth;
        if(size %2 != 0)
        {
            if (_gridHeight % 2 != 0) _gridHeight--;
            else _gridWidth++;
        }
    }

    private List<Sprite> CreateIconList()
    {
        List<Sprite> resultList = new List<Sprite>();
        int size = _gridHeight * _gridWidth;
        Sprite[] iconarray = new Sprite[size/2];


        Array.Copy(_icons, iconarray, size / 2);

        for(int i=0; i< size; i++)
        {

        }

        return resultList;
    }

}

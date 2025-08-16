using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private Sprite _backFace;
    [SerializeField]
    private Sprite _frontFace;
    [SerializeField]
    private Sprite _iconImage;
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private int ID;

    [SerializeField]
    private bool isSelected = false;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        //disabling card (changing icon opacity to 0.5)
        //_animator.SetTrigger("Disabled");
        //selecting the card(rotating the card and showing the icon)
        //_animator.SetTrigger("Selected");
        //StartCoroutine(TestCoroutine());
    }

    public void onCardClick()
    {
        
        if (!isSelected)
        {
            ShowCard();
        }
        else
        {
            HideCard();
        }
        isSelected = !isSelected;
    }


    IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        ShowCard();
        yield return new WaitForSeconds(1.0f);
        HideCard();
        yield break;
    }

    public void ShowCard()
    {
        _animator.SetTrigger("Selected");
    }

    public void HideCard()
    {
        _animator.SetTrigger("Pressed");
    }

    public void DisableCard()
    {

    }
}

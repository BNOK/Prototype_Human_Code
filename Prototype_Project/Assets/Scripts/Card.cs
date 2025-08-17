using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private Sprite _backFace;
    [SerializeField]
    private Sprite _frontFace;
    [SerializeField]
    private Image _iconImageComponent;

    [SerializeField]
    private Animator _animator;
    private Button _button;

    [SerializeField]
    private int _id = -1;

    [SerializeField]
    private bool isSelected = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        GetCardSize();
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
        
    }

    public void ShowCard()
    {
        _animator.SetTrigger("Selected");
        isSelected = !isSelected;
    }

    public void HideCard()
    {
        _animator.SetTrigger("Pressed");
        isSelected = !isSelected;
    }

    public void DisableCard()
    {
        _animator.SetTrigger("Disabled");
        _button.interactable = false;
    }

    public float[] GetCardSize()
    {
        float[] result = new float[2];
        RectTransform rect = GetComponent<RectTransform>();
        result[0] = rect.sizeDelta.x;
        result[1] = rect.sizeDelta.y;
        Debug.Log(result);
        return result;
    }

    public int GetID()
    {
        return _id;
    }

    public void SetupCard(int id, Sprite iconImage, UnityAction callback)
    {
        _id = id;
        _iconImageComponent.sprite = iconImage;
        _button.onClick.AddListener(callback);
    }

   
}

using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class UICard : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<Ability, int> OnAbilitySelect;
    
    public Transform ScaledCardHolder;
    public CardUIData UIData;

    [SerializeField] private float scaleOnHover = 2.5f, scaleOnSelected = 1f;

    public float ScaleOnSelected { get { return scaleOnSelected; } private set { scaleOnSelected = value; } }
    public int Id { get; private set; }

    public void SetId(int id)
    {
        Id = id;
        UIData.SetId(id);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (UIData.Ability != null)
        {
            OnAbilitySelect?.Invoke(UIData.Ability, Id);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ScaledCardHolder.localScale = Vector3.one * scaleOnHover;
        transform.SetAsLastSibling();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ScaledCardHolder.localScale = Vector3.one;
        transform.SetSiblingIndex(Id);
    }
}
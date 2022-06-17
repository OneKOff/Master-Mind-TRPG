using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class UIAbility : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<Ability, int> OnAbilitySelect;

    public Transform ScaledCardHolder;
    public CardUIData UIData;

    [SerializeField] private float scaleOnHover = 2.5f, scaleOnSelected = 1f;
    [SerializeField] private Image portrait;
    [SerializeField] private TextMeshProUGUI epCostHolder, tpCostHolder;

    public float ScaleOnSelected { get { return scaleOnSelected; } private set { scaleOnSelected = value; } }
    public int Id { get; private set; }
    public Ability Ability { get; private set; }

    public void SetId(int id)
    {
        Id = id;
        UIData.SetId(id);
    }

    public void SetAbility(Ability ability)
    {
        var abilityInfo = ability.AbilityData;

        Ability = ability;

        if (epCostHolder != null) epCostHolder.text = abilityInfo.epCost.ToString();
        if (tpCostHolder != null) tpCostHolder.text = abilityInfo.tpCost.ToString();
        if (portrait != null) portrait.sprite = abilityInfo.icon;

        UIData.SetAbility(ability);
    }

    public void SetAbility(AbilityHolder.AbilityType abilityType)
    {
        SetAbility(GameController.Instance.AbilityHolder.
            GetAbility(abilityType));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Ability != null)
        {
            OnAbilitySelect?.Invoke(Ability, Id);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ScaledCardHolder.gameObject.SetActive(true);
        ScaledCardHolder.localScale = Vector3.one * scaleOnHover;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ScaledCardHolder.localScale = Vector3.one;
        ScaledCardHolder.gameObject.SetActive(false);
    }
}

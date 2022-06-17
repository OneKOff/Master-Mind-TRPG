using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUIData : MonoBehaviour
{
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI cardName;
    
    public Ability Ability { get; protected set; }
    public int Id { get; protected set; }

    protected AbilityData _abilityData;

    public void SetId(int id) { Id = id; }

    public virtual void SetAbility(Ability ability)
    {
        if (ability != null)
        {
            Ability = ability;

            SetUIData();
        }
        else
        {
            ResetSelection();
        }
    }

    public void SetAbility(AbilityHolder.AbilityType abilityType)
    {
        SetAbility(GameController.Instance.AbilityHolder.
            GetAbility(abilityType));
    }

    protected virtual void SetUIData()
    {
        _abilityData = Ability.AbilityData;

        if (icon != null) icon.sprite = _abilityData.icon;
        if (cardName != null) cardName.text = _abilityData.cardName;
    }
    protected void ResetSelection()
    {
        if (GameController.Instance.UIController.selectedAbilityId == Id)
        {
            transform.localScale = Vector3.one;
            if (TryGetComponent(out Image image))
            {
                image.color = Color.white;
            }
        }
        gameObject.SetActive(false);
    }
}

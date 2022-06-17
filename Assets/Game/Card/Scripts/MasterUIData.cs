using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MasterUIData : CardUIData
{
    [SerializeField] protected TextMeshProUGUI health, healthRegen;
    [SerializeField] protected TextMeshProUGUI energy, energyRegen;
    [SerializeField] protected TextMeshProUGUI time;
    [SerializeField] protected TextMeshProUGUI power, defence;

    public UnitAbilityUIData[] abilitiesData = new UnitAbilityUIData[3];

    protected UnitData _unitData;

    public override void SetAbility(Ability ability)
    {
        if (ability != null && Ability is SummonAbility)
        {
            Ability = ability;

            SetUIData();
        }
        else
        {
            ResetSelection();
        }
    }

    protected override void SetUIData()
    {
        base.SetUIData();

        _unitData = GameController.Instance.UnitHolder.
                GetUnitData(((SummonAbility)Ability).UnitType);

        if (health != null) health.text = _unitData.maxHealth.ToString();
        if (healthRegen != null) healthRegen.text = _unitData.hpRegen.ToString();
        if (energy != null) energy.text = _unitData.maxEnergy.ToString();
        if (energyRegen != null) energyRegen.text = _unitData.epRegen.ToString();
        if (time != null) time.text = _unitData.maxTime.ToString();
        if (power != null) power.text = _unitData.power.ToString();
        if (defence != null) defence.text = _unitData.defence.ToString();

        for (var i = 0; i < abilitiesData.Length; i++)
        {
            abilitiesData[i].SetAbility(_unitData.innerAbilities[i]);
        }
    }
}

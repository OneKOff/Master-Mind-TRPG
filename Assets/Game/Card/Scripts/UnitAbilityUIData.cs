using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitAbilityUIData : AbilityUIData
{
    [SerializeField] protected TextMeshProUGUI description;
    [SerializeField] protected TextMeshProUGUI epCost, tpCost;
    [SerializeField] protected TextMeshProUGUI range, area;

    protected override void SetUIData()
    {
        base.SetUIData();
        
        if (description != null)
        {
            if (_abilityData.values.Length > 0)
            {
                var values = new object[_abilityData.values.Length];
                for (var i = 0; i < values.Length; i++)
                {
                    values[i] = _abilityData.values[i];
                }

                description.text = string.Format(_abilityData.description, values);
            }
            else
            {
                description.text = _abilityData.description;
            }
        }
        if (epCost != null) epCost.text = _abilityData.epCost.ToString();
        if (tpCost != null) tpCost.text = _abilityData.tpCost.ToString();
        if (range != null) range.text = $"{_abilityData.minRange.ToString()}\n-\n{_abilityData.maxRange.ToString()}";
        if (area != null) area.text = $"{_abilityData.minAreaRange.ToString()}\n-\n{_abilityData.maxAreaRange.ToString()}";
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServantUIData : MasterUIData
{
    [SerializeField] protected TextMeshProUGUI epCost, tpCost;

    protected override void SetUIData()
    {
        base.SetUIData();

        if (epCost != null) epCost.text = _abilityData.epCost.ToString();
        if (tpCost != null) tpCost.text = _abilityData.tpCost.ToString();
    }
}

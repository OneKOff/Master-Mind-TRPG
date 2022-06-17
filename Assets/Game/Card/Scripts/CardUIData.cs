using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

public class CardUIData : AbilityUIData
{
    [SerializeField] protected GameObject[] aspectCovers = new GameObject[4];
    [SerializeField] protected TextMeshProUGUI[] aspectDedications = new TextMeshProUGUI[4];

    protected override void SetUIData()
    {
        base.SetUIData();

        for (var i = 0; i < 4; i++)
        {
            if (aspectDedications[i] != null)
            {
                aspectDedications[i].text = $"{_abilityData.Dedications[i].Value}";
                aspectDedications[i].gameObject.SetActive(_abilityData.Dedications[i].IsUsable);
            }

            if (aspectCovers[i] != null)
            {
                aspectCovers[i].SetActive(!_abilityData.Dedications[i].IsUsable);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnQueueUnitIcon : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image unitPortrait;

    public void ChangeBackgroundColor(Color color)
    {
        background.color = color;
    }

    public void ChangePortrait(Sprite sprite)
    {
        unitPortrait.sprite = sprite;
    }
}

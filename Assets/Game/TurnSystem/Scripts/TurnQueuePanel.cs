using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnQueuePanel : MonoBehaviour
{
    [SerializeField] private TurnQueueUnitIcon unitPanelPrefab;

    [SerializeField] private TurnQueueUnitIcon[] queuePanels = new TurnQueueUnitIcon[TurnManager.QUEUE_CAPACITY];

    public void SetQueue()
    {
        var masterIdQueue = GameController.Instance.TurnManager.GetNewQueue();
        for (var i = 0; i < queuePanels.Length; i++)
        {
            if (!GameController.Instance.EntityManager.FindMasterUnitByMasterId(masterIdQueue[i], out var masterUnit)) { continue; }
            
            queuePanels[i].ChangePortrait(masterUnit.UnitStats.Portrait);
            queuePanels[i].ChangeBackgroundColor(GameController.Instance.TurnManager.TeamColors[masterUnit.UnitStats.TeamId - 1]);
        }
    }
}
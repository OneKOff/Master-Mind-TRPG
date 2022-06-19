using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public const int QUEUE_CAPACITY = 8;
    
    public List<int> TeamIds = new List<int>();
    public List<Color> TeamColors = new List<Color>()
    {
        Color.blue,
        Color.red
    };
    [HideInInspector] public List<MasterTurnDelay> Delays = new List<MasterTurnDelay>();
    [HideInInspector] public List<MasterTurnDelay> DelayCopies = new List<MasterTurnDelay>();

    [Header("Turn Time")]
    public Timer TurnTimer;
    public float TurnDuration = 15f;

    [HideInInspector] public int MasterIndex;
    
    public int CurrentMasterId => Delays[MasterIndex].MasterId;
    public int CurrentTeamId
    {
        get
        {
            if (GameController.Instance.EntityManager.FindMasterUnitByMasterId(Delays[MasterIndex].MasterId,
                out MasterUnit masterUnit))
            {
                return 0;
            }
            return masterUnit.TeamId;
        }
    }

    public static int CalculateDelay(int time)
    {
        return 1000000 / time;
    }
    
    private void Start()
    {
        // Set on when remaking system
        Task.Delay(1000);
        SetTimer();
        SetInitialMasterDelays();
        GameController.Instance.UIController.TurnQueuePanel.SetQueue();
    }
    private void OnDisable()
    {
        TurnTimer.OnTimerElapsed -= ResetStates;
    }
    
    private void SetTimer()
    {
        if (TurnTimer == null)
        {
            TurnTimer = gameObject.AddComponent<Timer>();
        }

        TurnTimer.Reset(TurnDuration);

        TurnTimer.OnTimerElapsed += ResetStates;
    }

    public void SetInitialMasterDelays()
    {
        AddMasterDelays(GameController.Instance.EntityManager.MasterUnits.ToArray());
    }

    public void AddMasterDelays(MasterUnit[] masterUnits)
    {
        var i = 0;
        foreach (var masterUnit in masterUnits)
        {
            var newDelay = new MasterTurnDelay(masterUnit.UnitStats.MasterId, CalculateDelay(masterUnit.UnitStats.MaxTime));
            Debug.Log($"Delay {i}: {newDelay.RemainingDelay}");
            Delays.Add(newDelay);
            i++;
        }
    }

    private void ResetStates()
    {
        MasterIndex = FindLowestDelayWithOffset(Delays, 0);

        ReduceAllDelays(Delays[MasterIndex].RemainingDelay, MasterIndex);
        
        GameController.Instance.UIController.TurnQueuePanel.SetQueue();
        GameController.Instance.SelectionManager.ResetSelections(SelectionStage.None);

        TurnTimer.Reset();
    }

    public int[] GetNewQueue()
    {
        var queue = new int[QUEUE_CAPACITY];
        var delay = 0;
        var lowestIndex = 0;

        DelayCopies.Clear();
        foreach (var d in Delays)
        {
            DelayCopies.Add(d);
        }
        
        for (var i = 0; i < QUEUE_CAPACITY; i++)
        {
            lowestIndex = FindLowestDelayWithOffset(DelayCopies, delay);
            //delay += DelayCopies[lowestIndex].RemainingDelay;
            queue[i] = DelayCopies[lowestIndex].MasterId;

            Debug.Log($"Lowest Index {lowestIndex}");
            Debug.Log($"Queue {i}: {DelayCopies[lowestIndex].MasterId} - {delay}");

            ReduceAllDelayCopies(DelayCopies[lowestIndex].RemainingDelay, lowestIndex);
        }

        DelayCopies.Clear();

        return queue;
    }

    private int FindLowestDelayWithOffset(List<MasterTurnDelay> delays, int offset)
    {
        var lowestDelayIndex = 0;

        for (var i = 0; i < delays.Count; i++)
        {
            var currentDelay = delays[i].RemainingDelay - offset;
            var lowestDelay = delays[lowestDelayIndex].RemainingDelay - offset;

            if (currentDelay < lowestDelay)
            {
                lowestDelayIndex = i;
            }
        }

        return lowestDelayIndex;
    }
    
    private int FindLowestDelayWithOffsetPositive(List<MasterTurnDelay> delays, int offset)
    {
        var lowestDelayIndex = 0;

        for (var i = 0; i < delays.Count; i++)
        {
            var currentDelay = delays[i].RemainingDelay - offset;
            var lowestDelay = delays[lowestDelayIndex].RemainingDelay - offset;

            if (currentDelay < lowestDelay && currentDelay > 0)
            {
                lowestDelayIndex = i;
            }
        }

        return lowestDelayIndex;
    }

    private void ReduceAllDelays(int amount, int lowestIndex)
    {
        for (var i = 0; i < Delays.Count; i++)
        {
            Delays[i].ReduceDelay(amount);
        }

        Delays[lowestIndex].RemainingDelay += Delays[lowestIndex].InitialDelay;
    }
    private void ReduceAllDelayCopies(int amount, int lowestIndex)
    {
        for (var i = 0; i < DelayCopies.Count; i++)
        {
            DelayCopies[i].ReduceDelay(amount);
        }

        DelayCopies[lowestIndex].RemainingDelay += DelayCopies[lowestIndex].InitialDelay;
    }
}
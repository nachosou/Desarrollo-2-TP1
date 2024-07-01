using System.Collections.Generic;
using UnityEngine;

public class FinalBossFSM : MonoBehaviour
{
    [SerializeField] private FinalBoss finalBoss;

    [SerializeField] private List<FinalBossState> states = new List<FinalBossState>();
    private FinalBossState currentState;

    void Start()
    {
        if (states.Count > 0)
            ChangeState(states[0]);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(finalBoss);
        }
    }

    public void ChangeState(FinalBossState state)
    {
        if (currentState != null)
        {
            currentState.Exit(finalBoss);
        }

        currentState = state;
        currentState.Enter(finalBoss);
    }
}

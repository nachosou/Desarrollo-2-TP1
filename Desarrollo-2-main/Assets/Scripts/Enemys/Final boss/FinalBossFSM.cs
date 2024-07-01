using System.Collections.Generic;
using UnityEngine;

public class FinalBossFSM : MonoBehaviour
{
    [Tooltip("Reference to the FinalBoss script controlling the boss behavior.")]
    [SerializeField] private FinalBoss finalBoss;

    [Tooltip("List of states available for the final boss.")]
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

    /// <summary>
    /// Changes the current state of the final boss FSM to the specified state
    /// </summary>
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

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBossState : ScriptableObject
{
    [Tooltip("List of states that this state can transition to.")]
    [SerializeField] public List<FinalBossState> states = new List<FinalBossState>();

    /// <summary>
    /// Called when entering this state
    /// </summary>
    public virtual void Enter(FinalBoss finalBoss) { }

    /// <summary>
    /// Called every frame to update the state's behavior
    /// </summary>
    public virtual void UpdateState(FinalBoss finalBoss) { }

    /// <summary>
    /// Called when exiting this state
    /// </summary>
    public virtual void Exit(FinalBoss finalBoss) { }
}

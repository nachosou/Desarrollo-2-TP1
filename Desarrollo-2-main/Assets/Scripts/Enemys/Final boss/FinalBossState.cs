using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBossState : ScriptableObject
{
    [SerializeField] public List<FinalBossState> states = new List<FinalBossState>();

    public virtual void Enter(FinalBoss finalBoss) { }

    public virtual void UpdateState(FinalBoss finalBoss) { }

    public virtual void Exit(FinalBoss finalBoss) { }
}

using UnityEngine;

/// <summary>
/// Represents the teleport state behavior of the final boss FSM
/// </summary>
[CreateAssetMenu(fileName = "TeleportState", menuName = "FSM/States/Teleport", order = 0)]
public class FinalBossTeleportState : FinalBossState
{
    public override void Enter(FinalBoss finalBoss)
    {
        finalBoss.TeleportRandomly();
        finalBoss.GetComponent<FinalBossFSM>().ChangeState(states[0]);
    }
}

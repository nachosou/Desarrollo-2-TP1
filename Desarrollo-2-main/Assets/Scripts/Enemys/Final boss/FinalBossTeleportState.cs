using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "FSM/States/Teleport", order = 0)]
public class FinalBossTeleportState : FinalBossState
{
    public override void Enter(FinalBoss finalBoss)
    {
        finalBoss.TeleportRandomly();
        finalBoss.GetComponent<FinalBossFSM>().ChangeState(states[0]);
    }
}

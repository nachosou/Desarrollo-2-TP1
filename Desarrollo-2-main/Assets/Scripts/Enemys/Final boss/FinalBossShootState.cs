using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "FSM/States/Shoot", order = 0)]
public class FinalBossShootState : FinalBossState
{
    public override void Enter(FinalBoss finalBoss)
    {
        finalBoss.ShootProjectile();
        finalBoss.GetComponent<FinalBossFSM>().ChangeState(states[0]);
    }
}

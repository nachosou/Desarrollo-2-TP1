using UnityEngine;

/// <summary>
/// Represents the idle state behavior of the final boss FSM
/// </summary>
[CreateAssetMenu(fileName = "IdleState", menuName = "FSM/States/Idle", order = 0)]
public class IdleState : FinalBossState
{
    [Tooltip("Cooldown time between actions.")]
    public float actionCoolDown;

    private float timer = 0;

    public override void Enter(FinalBoss finalBoss)
    {
        timer = 0;
    }

    public override void UpdateState(FinalBoss finalBoss)
    {
        timer += Time.deltaTime;

        if (timer >= actionCoolDown)
        {
            int randomIndex = Random.Range(0, states.Count);
            finalBoss.GetComponent<FinalBossFSM>().ChangeState(states[randomIndex]);
        }
    }
}

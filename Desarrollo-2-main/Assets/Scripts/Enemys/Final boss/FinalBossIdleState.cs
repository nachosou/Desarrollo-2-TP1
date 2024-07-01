using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "FSM/States/Idle", order = 0)] 
public class IdleState : FinalBossState
{
    public float actionCoolDown;
    float timer = 0;

    public override void Enter(FinalBoss finalBoss)
    {
        timer = 0;
    }

    public override void UpdateState(FinalBoss finalBoss)
    {
        timer += Time.deltaTime;

        if(actionCoolDown <= timer)
        {
            int randomIndex = Random.Range(0, states.Count);

            finalBoss.GetComponent<FinalBossFSM>().ChangeState(states[randomIndex]);   
        }
    }
}

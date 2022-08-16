using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : EnemyBaseState
{
    EnemyBehaviour EnemyBehaviour;
    public override void EnterState(EnemyStateManager enemy)
    {
        EnemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        EnemyBehaviour.AimAtPlayer();
        EnemyBehaviour.FireBullet();
        if (!EnemyBehaviour.InRange())
        {
            enemy.SwitchState(enemy.IdleState);
        }
    }
}

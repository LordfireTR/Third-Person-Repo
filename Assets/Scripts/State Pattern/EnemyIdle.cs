using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyBaseState
{
    EnemyBehaviour EnemyBehaviour;
    float deactivationTimer;

    public override void EnterState(EnemyStateManager enemy)
    {
        EnemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
        deactivationTimer = 1.0f;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        deactivationTimer -= Time.deltaTime;

        if (EnemyBehaviour.WaitBeforeDeactivate(deactivationTimer))
        {
            EnemyBehaviour.IdlePose();
        }
        
        if (EnemyBehaviour.InRange())
        {
            enemy.SwitchState(enemy.ActiveState);
        }
    }
}

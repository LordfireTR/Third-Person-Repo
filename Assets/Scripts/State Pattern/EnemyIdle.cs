using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Idle");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if ((enemy.turret.position - enemy.player.position).magnitude <= enemy.turretRange)
        {
            enemy.SwitchState(enemy.ActiveState);
        }
    }
}

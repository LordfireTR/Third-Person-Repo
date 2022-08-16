using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Active");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.turret.LookAt(enemy.player);
        if (enemy.turret.localEulerAngles.x < 360.0f)
        {
            if (enemy.turret.localEulerAngles.x < 345.0f && enemy.turret.localEulerAngles.x > 180.0f)
            {
                enemy.turret.localEulerAngles = new Vector3(345.0f, enemy.turret.localEulerAngles.y, enemy.turret.localEulerAngles.z);
            }
            else if(enemy.turret.localEulerAngles.x > 10.0f && enemy.turret.localEulerAngles.x <= 180.0f)
            {
                enemy.turret.localEulerAngles = new Vector3(10.0f, enemy.turret.localEulerAngles.y, enemy.turret.localEulerAngles.z);
            }
        }

        if ((enemy.turret.position - enemy.player.position).magnitude > enemy.turretRange)
        {
            enemy.SwitchState(enemy.IdleState);
        }
    }
}

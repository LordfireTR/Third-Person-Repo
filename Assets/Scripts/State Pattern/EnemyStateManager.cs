using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;
    public EnemyActive ActiveState = new EnemyActive();
    public EnemyIdle IdleState = new EnemyIdle();

    public Transform player;
    public Transform turret;
    public float turretRange = 20.0f;
    
    void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);
    }
    
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}

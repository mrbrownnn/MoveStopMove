using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyPatrol : IState
{
    public void OnEnter(EnemyController enemy)
    {
        enemy.FindNextDestination();
        enemy.RestartTimeCounting();
    }
    public void OnExecute(EnemyController enemy)
    {
        enemy.OnRun();
        enemy.EnemyMovement();
        enemy.CheckPatroltoAttack();
        enemy.CheckArriveDestination();
    }
    public void OnExit(EnemyController enemy)
    {
        enemy.EnemyStopMoving();
        enemy.OnResetAllTrigger();
    }
}

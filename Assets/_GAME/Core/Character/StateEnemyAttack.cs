using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyAttack : IState
{
    public void OnEnter(EnemyController enemy)
    {
        enemy.OnResetAllTrigger();
        enemy.attack();
        enemy.EnemyStopMoving();
        enemy.RestartTimeCounting();
    }
    public void OnExecute(EnemyController enemy)
    {
        enemy.CheckIfAttackIsDone();
    }
    public void OnExit(EnemyController enemy)
    {
        enemy.OnResetAllTrigger();
    }
}

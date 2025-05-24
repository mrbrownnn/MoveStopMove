using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerIdle : IStatePlayer
{
    public void OnEnter(PlayerController player)
    {
        player.OnIdle();
    }
    public void OnExecute(PlayerController player)
    {
        player.CheckIdleToPatrol();
        player.CheckIdletoAttack();
    }
    public void OnExit(PlayerController player)
    {
        player.OnResetAllTrigger();
    }
}

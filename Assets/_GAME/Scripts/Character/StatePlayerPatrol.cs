using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerPatrol : IStatePlayer
{
    public void OnEnter(PlayerController player)
    {
        player.OnRun();
    }
    public void OnExecute(PlayerController player)
    {
        player.move();
        player.CheckPatrolToIdle();
    }
    public void OnExit(PlayerController player)
    {
        player.OnResetAllTrigger();
    }
}

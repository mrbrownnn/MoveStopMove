using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerAttack : IStatePlayer
{
    public void OnEnter(PlayerController player)
    {
        player.OnAttack();
    }
    public void OnExecute(PlayerController player)
    {
        player.attack();
        player.CheckIdleToPatrol();
    }
    public void OnExit(PlayerController player)
    {
        player.OnResetAllTrigger();
    }
}

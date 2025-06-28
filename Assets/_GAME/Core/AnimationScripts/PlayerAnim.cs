using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : CharacterAnim,ISubscribers,IInitializeVariables
{
    [SerializeField] private Transform Player;
    private PlayerController playerController;
    
    private void OnEnable()
    {
        InitializeVariables();
        SubscribeEvent();
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }
    public void SubscribeEvent()
    {
        playerController.OnAttack += AttackAnimation;
        playerController.OnDance += DanceAnimation;
        playerController.OnIdle += IdleAnimation;
        playerController.OnDeath += DeathAnimation;
        playerController.OnWin += WinAnimation;
        playerController.OnRun += RunAnimation;
        playerController.OnUlti += UltiAnimation;
        playerController.OnResetAllTrigger += ResetAllTriggerAnim;
    }
    public void UnsubscribeEvent()
    {
        playerController.OnAttack -= AttackAnimation;
        playerController.OnDance -= DanceAnimation;
        playerController.OnIdle -= IdleAnimation;
        playerController.OnDeath -= DeathAnimation;
        playerController.OnWin -= WinAnimation;
        playerController.OnRun -= RunAnimation;
        playerController.OnUlti -= UltiAnimation;
        playerController.OnResetAllTrigger -= ResetAllTriggerAnim;
    }
    public void InitializeVariables()
    {
        playerController = Player.GetComponent<PlayerController>();
    }
}

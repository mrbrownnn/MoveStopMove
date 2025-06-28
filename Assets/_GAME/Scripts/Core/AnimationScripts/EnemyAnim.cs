using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : CharacterAnim, ISubscribers
{
    [SerializeField] private Transform enemy;
    private EnemyController enemyController;
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
        enemyController.OnAttack += AttackAnimation;
        enemyController.OnRun += RunAnimation;
        enemyController.OnIdle += IdleAnimation;
        enemyController.OnDeath += DeathAnimation;
        enemyController.OnWin += WinAnimation;
        enemyController.OnDance += DanceAnimation;
        enemyController.OnUlti += UltiAnimation;
        enemyController.OnResetAllTrigger += ResetAllTriggerAnim;
    }

    public void UnsubscribeEvent()
    {
        enemyController.OnAttack -= AttackAnimation;
        enemyController.OnRun -= RunAnimation;
        enemyController.OnIdle -= IdleAnimation;
        enemyController.OnDeath -= DeathAnimation;
        enemyController.OnWin -= WinAnimation;
        enemyController.OnDance -= DanceAnimation;
        enemyController.OnResetAllTrigger -= ResetAllTriggerAnim;

    }
    public void InitializeVariables()
    {
        enemyController = enemy.GetComponent<EnemyController>();
    }
}

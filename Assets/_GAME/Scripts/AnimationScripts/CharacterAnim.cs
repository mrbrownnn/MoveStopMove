using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    [HideInInspector] public enum CharacterAnimState { Attack, Dance, Idle, Death, Run, Win, Ulti }
    public CharacterAnimState lastState;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        animator = GetComponent<Animator>();
        lastState = CharacterAnimState.Idle;
    }

    #region Set Character Animation
    public void SetAnim(CharacterAnimState _CharacterAnimation)
    {
        if(_CharacterAnimation!= lastState)
        {
            switch (_CharacterAnimation)
            {
                case CharacterAnimState.Attack:
                    animator.SetTrigger("Attack");
                    break;
                case CharacterAnimState.Dance:
                    animator.SetTrigger("Dance");
                    break;
                case CharacterAnimState.Idle:
                    animator.SetTrigger("Idle");
                    break;
                case CharacterAnimState.Death:
                    animator.SetTrigger("Death");
                    break;
                case CharacterAnimState.Run:
                    animator.SetTrigger("Run");
                    break;
                case CharacterAnimState.Win:
                    animator.SetTrigger("Win");
                    break;
                case CharacterAnimState.Ulti:
                    animator.SetTrigger("Ulti");
                    break;
            }
            lastState = _CharacterAnimation;
        }
    }
    #endregion


    #region Play Character Animation
    public void AttackAnimation()
    {
        SetAnim(CharacterAnimState.Attack);
    }
    public void DanceAnimation()
    {
        SetAnim(CharacterAnimState.Dance);
    }
    public void IdleAnimation()
    {
        SetAnim(CharacterAnimState.Idle);
    }
    public void DeathAnimation()
    {
        SetAnim(CharacterAnimState.Death);
    }
    public void RunAnimation()
    {
        SetAnim(CharacterAnimState.Run);
    }
    public void WinAnimation()
    {
        SetAnim(CharacterAnimState.Win);
    }
    public void UltiAnimation()
    {
        SetAnim(CharacterAnimState.Ulti);
    }
    public void ResetAllTriggerAnim()
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Dance");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Death");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Win");
        animator.ResetTrigger("Ulti");
    }
    #endregion
}

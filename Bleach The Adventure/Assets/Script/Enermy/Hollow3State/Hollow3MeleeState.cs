using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hollow3MeleeState : Hollow3IEnermyState
{
    private float attackTimer;
    private float attackCoolDown = 3;
    private bool canAttack = true;

    private Hollow3 enermy;
    public void Enter(Hollow3 enermy)
    {
        this.enermy = enermy;
    }

    public void Execute()
    {
        Attack(); 
        if(enermy.InThrowRange && !enermy.InMeleeRange)
        {
            enermy.ChangeState(new Hollow3RangedState());
        }
        else if (enermy.Target == null)
        {
            enermy.ChangeState(new Hollow3IdeState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D orther)
    {
        
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCoolDown)
        {
            canAttack = true;
            attackTimer = 0;
        }

        if (canAttack)
        {
            canAttack = false;
            enermy.MyAnimator.SetTrigger("attack_stand1");
        }
    }
}

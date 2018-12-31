using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hollow3RangedState : Hollow3IEnermyState
{
    Hollow3 enermy;

    private float throwTimer;
    private float throwCoolDown = 3;
    private bool canThrow = true;

    public void Enter(Hollow3 enermy)
    {
        this.enermy = enermy;
    }

    public void Execute()
    {
        ThrowAir();
        if (enermy.InMeleeRange)
        {
            enermy.ChangeState(new Hollow3MeleeState());
        }
        else
        if (enermy.Target != null)
        {
            enermy.Move();
        }
        else
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

    private void ThrowAir()
    {
        throwTimer += Time.deltaTime;

        if (throwTimer >= throwCoolDown)
        {
            canThrow = true;
            throwTimer = 0;
        }

        if (canThrow)
        {
            canThrow = false;
            enermy.MyAnimator.SetTrigger("attack_air");
        }
    }
}

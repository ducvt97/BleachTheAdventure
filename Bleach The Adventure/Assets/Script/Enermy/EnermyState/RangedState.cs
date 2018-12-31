using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnermyState
{
    GrandFisher enermy;

    private float throwTimer;
    private float throwCoolDown = 3;
    private bool canThrow = true;

    public void Enter(GrandFisher enermy)
    {
        this.enermy = enermy;
    }

    public void Execute()
    {
        ThrowAir();
        if (enermy.InMeleeRange)
        {
            enermy.ChangeState(new MeleeState());
        }
        else
        if (enermy.Target != null)
        {
            enermy.Move();
        }
        else
        {
            enermy.ChangeState(new IdeState());
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

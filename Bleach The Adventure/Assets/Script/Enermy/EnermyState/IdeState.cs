using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdeState : IEnermyState
{
    private GrandFisher enermy;

    private float idleTimer;

    private float idleDuration;

    public void Enter(GrandFisher enermy)
    {
        idleDuration = UnityEngine.Random.Range(1, 10);
        this.enermy = enermy;    
    }

    public void Execute()
    {
        Idle();

        if (enermy.Target != null)
        {
            enermy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Air"|| other.tag == "PlayerAttack")
        {
            enermy.Target = IchigoScript.Instance.gameObject;
        }
    }

    private void Idle()
    {
        enermy.MyAnimator.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            enermy.ChangeState(new PatrolState());
        }
    }
}

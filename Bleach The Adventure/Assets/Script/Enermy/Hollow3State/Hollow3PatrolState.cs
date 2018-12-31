using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hollow3PatrolState : Hollow3IEnermyState
{
    private Hollow3 enermy;

    private float patrolTimer;

    private float patrolDuration;

    public void Enter(Hollow3 enermy)
    {
        patrolDuration = UnityEngine.Random.Range(1, 10);
        this.enermy = enermy;
    }

    public void Execute()
    {
        Debug.Log("Patroling");
        Patrol();

        enermy.Move();

        if (enermy.Target != null && enermy.InThrowRange) 
        {
            enermy.ChangeState(new Hollow3RangedState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        
        if (other.tag == "Air" || other.tag == "Sword1" || other.tag == "Sword2")
        {
            enermy.Target = IchigoScript.Instance.gameObject;
        }
    }

    private void Patrol()
    {

        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            enermy.ChangeState(new Hollow3IdeState());
        }
    }
}

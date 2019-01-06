using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jibakurai : Minion
{
    public Collider2D attackLeft, attackRight;
    //private SoundManager sound;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        attackLeft.enabled = attackRight.enabled = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        isAwake = CheckRange();
        if (curHP <= 0)
        {
            state = 3;
            SetAction();
        }
        else
        {
            if (isAwake)
            {
                if (((target.transform.position.x < transform.position.x && !faceRight) ||
                    (target.transform.position.x > transform.position.x && faceRight)) &&
                    Math.Abs(target.transform.position.y - transform.position.y) < 1)
                    Flip();
                if (Math.Abs(target.transform.position.x - transform.position.x) < 1f)
                    Attack();
                else
                    Walk();
            }
            else
            {
                base.Update();
            }
        }
    }

    bool CheckRange()
    {
        var distance = Vector2.Distance(target.transform.position, this.transform.position);
        if (distance <= awakeRange)
        {
            return true;
        }
        return false;
    }

    public void Attack()
    {
        if (attackDelay > 0)
            attackDelay -= Time.deltaTime;
        else
        {
            state = 4;
            attackDelay = 1.5f;
            if (faceRight)
                attackRight.enabled = true;
            else
                attackLeft.enabled = true;
            SetAction();
        }

    }

    public void AlertObservers(string message)
    {
        switch (message)
        {
            case "AttackEnd":
                attackLeft.enabled = attackRight.enabled = false;
                state = 0;
                SetAction();
                break;
            case "Dead":
                Destroy(gameObject);
                break;
        }
    }
}

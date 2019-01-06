using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nemu : Minion
{
    public Collider2D attack1Left, attack1Right, attack2Left, attack2Right;
    //private SoundManager sound;

    public float actionDelay;
    private bool isBlocking;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        attack1Left.enabled = attack1Right.enabled = attack2Left.enabled = attack2Right.enabled = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        isAwake = CheckRange();
        if (curHP > 0)
        {
            if (isAwake)
            {
                if (((target.transform.position.x < transform.position.x && !faceRight) ||
                    (target.transform.position.x > transform.position.x && faceRight)) &&
                    Math.Abs(target.transform.position.y - transform.position.y) < 1)
                    Flip();
                if (Math.Abs(target.transform.position.x - transform.position.x) < 1f)
                    GetRandomAction();
                else
                    Walk();
            }
            else
            {
                base.Update();
            }
        }
        else
        {
            state = 3;
            SetAction();
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
    void GetRandomAction()
    {
        if (actionDelay > 0)
        {
            actionDelay -= Time.deltaTime;
        }
        else
        {
            actionDelay = 1f;
            var rnd = new System.Random();
            var action = rnd.Next(4, 7);
            switch (action)
            {
                case 4:
                    Attack1();
                    break;
                case 5:
                    Attack2();
                    break;
                case 6:
                    Block();
                    break;
            }
        }
    }
    public void Attack1()
    {
        state = 4;
        if (faceRight)
            attack1Right.enabled = true;
        else
            attack1Left.enabled = true;
        SetAction();
    }
    public void Attack2()
    {
        state = 5;
        if (faceRight)
            attack2Right.enabled = true;
        else
            attack2Left.enabled = true;
        SetAction();
    }
    public void Block()
    {
        isBlocking = true;
        state = 6;
        SetAction();
    }

    public void AlertObservers(string message)
    {
        switch (message)
        {
            case "Attack1End":
                attack1Left.enabled = attack1Right.enabled = false;
                state = 0;
                SetAction();
                break;
            case "Attack2End":
                attack2Left.enabled = attack2Right.enabled = false;
                state = 0;
                SetAction();
                break;
            case "BlockEnd":
                isBlocking = false;
                state = 0;
                SetAction();
                break;
            case "Dead":
                Destroy(gameObject);
                break;
        }
    }
}

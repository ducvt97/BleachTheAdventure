using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renji : Boss
{
    public Collider2D attackStand1, attackStand2, attackAir, attackSpecial1, attackSpecial2, attackSpecial3, skill1;
    public GameObject skill2;
    //private SoundManager sound;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        attackStand1.enabled = attackStand2.enabled = attackAir.enabled = 
            attackSpecial1.enabled = attackSpecial2.enabled = attackSpecial3.enabled = skill1.enabled = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (curHP > 0)
        {
            if (form == 1)
                Form2();
            else if (form == 2)
                Form2();
        }
        else
        {
            state = 3;
            SetAction();
        }
    }

    public void Form1()
    {
        if (((target.transform.position.x < transform.position.x && !faceRight) ||
                (target.transform.position.x > transform.position.x && faceRight)))
            Flip();
        if (!isAction)
        {
            if (Math.Abs(target.transform.position.x - transform.position.x) < 1f)
                if (Math.Abs(target.transform.position.y - transform.position.y) < 0.5f)
                    AttackStand();
                else
                    AttackAir();
            else if (Math.Abs(target.transform.position.x - transform.position.x) < 3f)
                Skill();
            else
                Move();
        }
    }
    public void Form2()
    {
        if (((target.transform.position.x < transform.position.x && !faceRight) ||
                (target.transform.position.x > transform.position.x && faceRight)))
            Flip();
        if (!isAction)
        {
            if (Math.Abs(target.transform.position.x - transform.position.x) < 1f)
            {
                if (Math.Abs(target.transform.position.y - transform.position.y) < 0.5f)
                    AttackStand();
                else
                    AttackAir();
            }
            else
            {
                if (Math.Abs(target.transform.position.x - transform.position.x) < 2f)
                    AttackSpecial();
                else
                {
                    if (Math.Abs(target.transform.position.x - transform.position.x) < 3f)
                        Skill();
                    else
                        Move();
                }
            }
        }
    }

    void AttackStand()
    {
        if (attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;
        }
        else
        {
            attackDelay = 1f;
            var rnd = new System.Random();
            var action = rnd.Next(0, 3);
            isAction = true;
            switch (action)
            {
                case 0:
                    AttackStand1();
                    break;
                case 1:
                    AttackStand2();
                    break;
                case 2:
                    Block();
                    break;
            }
        }
    }

    public void AttackStand1()
    {
        attackStand1.enabled = true;
        state = 6;
        SetAction();
    }
    public void AttackStand2()
    {
        attackStand2.enabled = true;
        state = 7;
        SetAction();
    }
    public void AttackAir()
    {
        if (attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;
        }
        else
        {
            attackDelay = 1f;
            attackAir.enabled = true;
            isAction = true;
            Jump();
            state = 8;
            SetAction();
        }
    }
    void AttackSpecial()
    {
        if (attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;
        }
        else
        {
            attackDelay = 1f;
            var rnd = new System.Random();
            var action = rnd.Next(0, 3);
            isAction = true;
            switch (action)
            {
                case 0:
                    AttackSpecial1();
                    break;
                case 1:
                    AttackSpecial2();
                    break;
                case 2:
                    AttackSpecial3();
                    break;
            }
        }
    }
    public void AttackSpecial1()
    {
        attackSpecial1.enabled = true;
        state = 9;
        SetAction();
    }
    public void AttackSpecial2()
    {
        attackSpecial2.enabled = true;
        state = 10;
        SetAction();
    }
    public void AttackSpecial3()
    {
        attackSpecial3.enabled = true;
        state = 11;
        SetAction();
    }
    public void Skill()
    {
        if (skillDelay > 0)
        {
            skillDelay -= Time.deltaTime;
        }
        else
        {
            skillDelay = 1.5f;
            var rnd = new System.Random();
            var action = rnd.Next(0, 2);
            isAction = true;
            switch (action)
            {
                case 0:
                    Skill1();
                    break;
                case 1:
                    Skill2();
                    break;
            }
        }
    }
    public void Skill1()
    {
        skill1.enabled = true;
        state = 12;
        SetAction();
    }
    public void Skill2()
    {
        state = 13;
        SetAction();
        GameObject skillClone;
        if (faceRight)
        {
            skillClone = Instantiate(skill2, new Vector3(transform.position.x - 1f, transform.position.y + 1f), Quaternion.Euler(new Vector3(0, 0, 0)));
            skillClone.GetComponent<RenjiSkill2>().Initialize(Vector2.left);
        }
        else
        {
            skillClone = Instantiate(skill2, new Vector3(transform.position.x + 1f, transform.position.y + 1f), Quaternion.Euler(new Vector3(0, 0, 0)));
            skillClone.GetComponent<RenjiSkill2>().Initialize(Vector2.right);
        }
    }
    public override void AlertObservers(string message)
    {
        base.AlertObservers(message);
        switch (message)
        {
            case "AttackStand1End":
                attackStand1.enabled = false;
                isAction = false;
                Stand();
                break;
            case "AttackStand2End":
                attackStand2.enabled = false;
                isAction = false;
                Stand();
                break;
            case "AttackAirEnd":
                attackStand2.enabled = false;
                isAction = false;
                Stand();
                break;
            case "AttackSpecial1End":
                attackSpecial1.enabled = false;
                isAction = false;
                Stand();
                break;
            case "AttackSpecial2End":
                attackSpecial2.enabled = false;
                isAction = false;
                Stand();
                break;
            case "AttackSpecial3End":
                attackSpecial3.enabled = false;
                isAction = false;
                Stand();
                break;
            case "Skill1End":
                skill1.enabled = false;
                isAction = false;
                Stand();
                break;
            case "Skill2End":
                isAction = false;
                Stand();
                break;
        }
    }
}

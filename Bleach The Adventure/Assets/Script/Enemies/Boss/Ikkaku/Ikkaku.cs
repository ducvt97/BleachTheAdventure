using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ikkaku : Boss
{
    public Collider2D attackStand1, attackStand2, attackStand3, attackAir1, attackAir2, skillAttack;
    public GameObject skill;
    //private SoundManager sound;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        attackStand1.enabled = attackStand2.enabled = attackStand3.enabled =
            attackAir1.enabled = attackAir2.enabled = skillAttack.enabled = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (curHP > 0)
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
        else
        {
            state = 3;
            SetAction();
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
            var action = rnd.Next(0, 4);
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
                    AttackStand3();
                    break;
                case 3:
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
    public void AttackStand3()
    {
        attackStand3.enabled = true;
        state = 8;
        SetAction();
    }
    void AttackAir()
    {
        if (attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;
        }
        else
        {
            attackDelay = 1f;
            var rnd = new System.Random();
            var action = rnd.Next(0, 2);
            isAction = true;
            switch (action)
            {
                case 0:
                    AttackAir1();
                    break;
                case 1:
                    AttackAir2();
                    break;
            }
        }
    }
    public void AttackAir1()
    {
        attackAir1.enabled = true;
        isAction = true;
        Jump();
        state = 9;
        SetAction();
    }
    public void AttackAir2()
    {
        attackAir1.enabled = true;
        isAction = true;
        Jump();
        state = 10;
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
            isAction = true;
            skillAttack.enabled = true;
            state = 11;
            SetAction();
            GameObject skillClone;
            if (faceRight)
            {
                skillClone = Instantiate(skill, new Vector3(transform.position.x - 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)));
                skillClone.GetComponent<IkkakuSkill>().Initialize(Vector2.left);
            }
            else
            {
                skillClone = Instantiate(skill, new Vector3(transform.position.x + 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)));
                skillClone.GetComponent<IkkakuSkill>().Initialize(Vector2.right);
            }
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
            case "AttackStand3End":
                attackStand3.enabled = false;
                isAction = false;
                Stand();
                break;
            case "AttackAir1End":
                attackAir1.enabled = false;
                isAction = false;
                Stand();
                break;
            case "AttackAir2End":
                attackAir2.enabled = false;
                isAction = false;
                Stand();
                break;
            case "SkillEnd":
                skillAttack.enabled = false;
                isAction = false;
                Stand();
                break;
        }
    }
}

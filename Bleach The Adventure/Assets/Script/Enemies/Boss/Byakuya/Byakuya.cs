﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byakuya : Boss
{
    public Collider2D attackStand1, attackStand2, attackAir1, attackAir2;
    public GameObject skill1, skill2, skill3, aura;
    //private SoundManager sound;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        attackStand1.enabled = attackStand2.enabled =
            attackAir1.enabled = attackAir2.enabled = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (curHP > 0)
        {
            if (CheckRange())
            {
                if (((target.transform.position.x < transform.position.x && !faceRight) ||
                    (target.transform.position.x > transform.position.x && faceRight)))
                {
                    Flip();
                }

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
                        if (Math.Abs(target.transform.position.x - transform.position.x) < 4f)
                        {
                            Skill();

                        }
                        else
                            Move();
                    }
                }
            }
        }
        else
        {
            if (form == 1)
                Transform();
            else if (form == 2)
                Dead();
        }
    }

    void Transform()
    {
        state = 13;
        SetAction();
    }
    void Aura()
    {
        GameObject auraClone;
        auraClone = Instantiate(aura, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        auraClone.GetComponent<ByakuyaAura>().Initialize(gameObject);
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
            var action = rnd.Next(0, 5);
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
                case 3:
                    Teleport();
                    break;
                case 4:
                    Jump();
                    break;
            }
        }
    }

    void AttackStand1()
    {
        attackStand1.enabled = true;
        state = 6;
        SetAction();
    }
    void AttackStand2()
    {
        attackStand2.enabled = true;
        state = 7;
        SetAction();
    }
    public override void Jump()
    {
        base.Jump();
        state = 12;
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
    void AttackAir1()
    {
        attackAir1.enabled = true;
        base.Jump();
        state = 8;
        SetAction();
    }
    void AttackAir2()
    {
        attackAir2.enabled = true;
        base.Jump();
        state = 9;
        SetAction();
    }

    void Skill()
    {
        if (skillDelay > 0)
        {
            skillDelay -= Time.deltaTime;
        }
        else
        {
            skillDelay = 1.5f;
            isAction = true;
            if (form == 1)
                Skill12();
            else if (form == 2)
            {
                var rnd = new System.Random();
                var action = rnd.Next(0, 2);
                switch (action)
                {
                    case 0:
                        Skill12();
                        break;
                    case 1:
                        Skill3();
                        break;
                }
            }
        }
    }
    void Skill12()
    {
        state = 10;
        SetAction();
        var rnd = new System.Random();
        var action = rnd.Next(0, 2);
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
    void Skill1()
    {
        GameObject skillClone;
        if (faceRight)
        {
            skillClone = Instantiate(skill1, new Vector3(transform.position.x - 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);
            skillClone.GetComponent<ByakuyaSkill1>().Initialize(Vector2.left);
        }
        else
        {
            skillClone = Instantiate(skill1, new Vector3(transform.position.x + 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);
            skillClone.GetComponent<ByakuyaSkill1>().Initialize(Vector2.right);
        }
    }
    void Skill2()
    {
        GameObject skillClone;
        if (faceRight)
        {
            skillClone = Instantiate(skill2, new Vector3(transform.position.x - 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);
            skillClone.GetComponent<ByakuyaSkill2>().Initialize(Vector2.left);
        }
        else
        {
            skillClone = Instantiate(skill2, new Vector3(transform.position.x + 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);
            skillClone.GetComponent<ByakuyaSkill2>().Initialize(Vector2.right);
        }
    }
    void Skill3()
    {
        state = 11;
        SetAction();
        GameObject skillClone;
        var pos = target.transform.position;
        if (faceRight)
        {
            for (int i = 0; i < 4; i++)
                skillClone = Instantiate(skill3, new Vector3(pos.x + i, pos.y + 6f), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else
        {
            for (int i = 0; i < 4; i++)
                skillClone = Instantiate(skill3, new Vector3(pos.x - i, pos.y + 6f), Quaternion.Euler(new Vector3(0, 0, 0)));
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
            case "JumpEnd":
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
                isAction = false;
                Stand();
                break;
            case "TransformEnd":
                curHP = maxHP;
                form = 2;
                isAction = false;
                Aura();
                Stand();
                break;
        }
    }
}

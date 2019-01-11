using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kenpachi : Boss
{
    public Collider2D attackStand1, attackStand2, attackStand3, attackAir1, attackAir2;
    public GameObject skill1, skill2, aura;
    //private SoundManager sound;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        attackStand1.enabled = attackStand2.enabled = attackStand3.enabled =
            attackAir1.enabled = attackAir2.enabled = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (form == 1)
            Form1();
        else if (form == 2)
            Form2();
    }

    void Form1()
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
                else
                    Move();
            }
        }
        else
        {
            Transform();
        }
        
    }
    void Form2()
    {
        if (curHP > 0)
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
                    if (Math.Abs(target.transform.position.x - transform.position.x) < 4f)
                        Skill();
                    else
                        Move();
                }
            }
        }
        else
            Dead();
        
    }
    void Transform()
    {
        state = 13;
        SetAction();
    }
    void Aura()
    {
        GameObject auraClone;
        auraClone = Instantiate(aura, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);
        auraClone.GetComponent<Aura>().Initialize(gameObject);
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
            var action = rnd.Next(0, 6);
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
                case 4:
                    Teleport();
                    break;
                case 5:
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
    void AttackStand3()
    {
        attackStand3.enabled = true;
        state = 8;
        SetAction();
    }
    public override void Jump()
    {
        base.Jump();
        state = 12;
        SetAction();
        isAction = true;
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
        isAction = true;
        base.Jump();
        state = 9;
        SetAction();
    }
    void AttackAir2()
    {
        attackAir1.enabled = true;
        isAction = true;
        base.Jump();
        state = 10;
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
            state = 11;
            SetAction();
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
    void Skill1()
    {
        GameObject skillClone;
        if (faceRight)
        {
            skillClone = Instantiate(skill1, new Vector3(transform.position.x - 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            skillClone.GetComponent<KenpachiSkill1>().Initialize(Vector2.left);
        }
        else
        {
            skillClone = Instantiate(skill1, new Vector3(transform.position.x + 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            skillClone.GetComponent<KenpachiSkill1>().Initialize(Vector2.right);
        }
    }
    void Skill2()
    {
        GameObject skillClone;
        if (faceRight)
        {
            skillClone = Instantiate(skill2, new Vector3(transform.position.x - 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            skillClone.GetComponent<KenpachiSkill2>().Initialize(Vector2.left);
        }
        else
        {
            skillClone = Instantiate(skill2, new Vector3(transform.position.x + 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            skillClone.GetComponent<KenpachiSkill2>().Initialize(Vector2.right);
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

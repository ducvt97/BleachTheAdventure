using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grimmjow : Boss
{
    // Start is called before the first frame update
    public Collider2D form1Attack1, form1Attack2, form2Attack1, form2Attack2, form2Skill1, form2Skill3;
    public GameObject form1Skill1, form1Skill2, form1Skill3, form2Skill2;
    //private SoundManager sound;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        form1Attack1.enabled = form1Attack2.enabled = form2Skill1.enabled = form2Skill3.enabled = false;
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
                {
                    Attack();
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
        else
        {
            Dead();
        }
    }

    void Attack()
    {
        if (attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;
        }
        else
        {
            if (form == 1)
            {
                var rnd = new System.Random();
                var action = rnd.Next(0, 5);
                isAction = true;
                switch (action)
                {
                    case 0:
                        Form1_Attack1();
                        break;
                    case 1:
                        Form1_Attack2();
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
            else if (form == 2)
            {
                var rnd = new System.Random();
                var action = rnd.Next(0, 5);
                isAction = true;
                switch (action)
                {
                    case 0:
                        Form2_Attack1();
                        break;
                    case 1:
                        Form2_Attack2();
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
            attackDelay = 1f;
        }
    }

    void Form1_Attack1()
    {
        form1Attack1.enabled = true;
        state = 6;
        SetAction();
    }
    void Form1_Attack2()
    {
        form1Attack2.enabled = true;
        state = 7;
        SetAction();
    }
    void Form2_Attack1()
    {
        form2Attack1.enabled = true;
        state = 6;
        SetAction();
    }
    void Form2_Attack2()
    {
        form2Attack2.enabled = true;
        state = 7;
        SetAction();
    }
    public override void Jump()
    {
        base.Jump();
        state = 15;
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
            {
                var rnd = new System.Random();
                var action = rnd.Next(0, 3);
                switch (action)
                {
                    case 0:
                        Form1_Skill1();
                        break;
                    case 1:
                        Form1_Skill2();
                        break;
                    case 2:
                        Form1_Skill3();
                        break;
                }
            }
            else if (form == 2)
            {
                var rnd = new System.Random();
                var action = rnd.Next(0, 3);
                switch (action)
                {
                    case 0:
                        Form2_Skill1();
                        break;
                    case 1:
                        Form2_Skill2();
                        break;
                    case 2:
                        Form2_Skill3();
                        break;
                }
            }
        }
    }

    void Form1_Skill1()
    {
        state = 8;
        SetAction();
        GameObject skillClone;
        if (faceRight)
        {
            skillClone = Instantiate(form1Skill1, new Vector3(transform.position.x - 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            skillClone.GetComponent<GrimmjowForm1_Skill>().Initialize(Vector2.left);
        }
        else
        {
            skillClone = Instantiate(form1Skill1, new Vector3(transform.position.x + 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 180)));
            skillClone.GetComponent<GrimmjowForm1_Skill>().Initialize(Vector2.right);
        }
    }
    void Form1_Skill2()
    {
        state = 9;
        SetAction();
        GameObject skillClone;
        if (faceRight)
        {
            skillClone = Instantiate(form1Skill2, new Vector3(transform.position.x - 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            skillClone.GetComponent<GrimmjowForm1_Skill>().Initialize(Vector2.left);
        }
        else
        {
            skillClone = Instantiate(form1Skill2, new Vector3(transform.position.x + 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 180)));
            skillClone.GetComponent<GrimmjowForm1_Skill>().Initialize(Vector2.right);
        }
    }
    void Form1_Skill3()
    {
        state = 10;
        SetAction();
        GameObject skillClone;
        if (faceRight)
        {
            skillClone = Instantiate(form1Skill3, new Vector3(transform.position.x - 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            skillClone.GetComponent<GrimmjowForm1_Skill>().Initialize(Vector2.left);
        }
        else
        {
            skillClone = Instantiate(form1Skill3, new Vector3(transform.position.x + 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 180)));
            skillClone.GetComponent<GrimmjowForm1_Skill>().Initialize(Vector2.right);
        }
    }
    void Form2_Skill1()
    {
        state = 8;
        SetAction();
    }
    void Form2_Skill2()
    {
        state = 9;
        SetAction();
        GameObject skillClone;
        var pos = target.transform.position;
        skillClone = Instantiate(form2Skill2, new Vector3(pos.x, pos.y + 6f), Quaternion.Euler(new Vector3(0, 0, 0)));
    }
    void Form2_Skill3()
    {
        state = 10;
        SetAction();
        damage += 1;
    }
    void Transform()
    {
        state = 15;
        SetAction();
    }
    void IncreasePower()
    {
        speed += speed < 0 ? -0.01f : 0.01f;
        teleRange += 40f;
        attackSpeed = 0.8f;
        skillSpeed = 1.2f;
        jumpPow += 30f;
    }
    public override void AlertObservers(string message)
    {
        base.AlertObservers(message);
        switch (message)
        {
            case "Transform":
                Transform();
                break;
            case "TransformEnd":
                curHP = maxHP;
                isAction = false;
                form = 2;
                Stand();
                break;
            case "Form1_Attack1End":
                form1Attack1.enabled = false;
                isAction = false;
                Stand();
                break;
            case "Form1_Attack2End":
                form1Attack2.enabled = false;
                isAction = false;
                Stand();
                break;
            case "Form2_Attack1End":
                form2Attack1.enabled = false;
                isAction = false;
                Stand();
                break;
            case "Form2_Attack2End":
                form2Attack2.enabled = false;
                isAction = false;
                Stand();
                break;
            case "Form2_Skill1":
                var newPos = new Vector2(transform.position.x + speed * 20f, transform.position.y);
                transform.position = newPos;
                break;
            case "JumpEnd":
                isAction = false;
                Stand();
                break;
            case "SkillEnd":
                isAction = false;
                Stand();
                break;
        }
    }
}

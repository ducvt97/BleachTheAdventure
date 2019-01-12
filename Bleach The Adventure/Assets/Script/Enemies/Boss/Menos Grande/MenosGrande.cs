using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenosGrande : Boss
{
    // Start is called before the first frame update
    public Collider2D attack1, attack2;
    public GameObject skill;
    //private SoundManager sound;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        attack1.enabled = attack2.enabled = false;
        teleRange = 100f;
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
                    Flip();
                if (!isAction)
                {
                    if (Math.Abs(target.transform.position.x - transform.position.x) < 1.5f)
                    {
                        Attack();
                    }
                    else
                    {
                        if (Math.Abs(target.transform.position.x - transform.position.x) < 4f)
                        {
                            SpecialAction();
                        }
                        else
                            Move();
                    }
                }
            }
        }
        else
        {
            Dead();
        }
    }
    public override void Move()
    {
        teleportDelay -= Time.deltaTime;
        if (teleportDelay < 0)
        {
            Jump();
            teleportDelay = 2f;
        }
        else
            Walk();
    }
    public override void Teleport()
    {
        isAction = true;
        state = 5;
        SetAction();
    }
    void Attack()
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
                    Attack1();
                    break;
                case 1:
                    Attack2();
                    break;
            }
        }
    }

    void Attack1()
    {
        attack1.enabled = true;
        state = 6;
        SetAction();
    }
    void Attack2()
    {
        attack2.enabled = true;
        state = 7;
        SetAction();
    }
    public override void Jump()
    {
        base.Jump();
        isAction = true;
        state = 9;
        SetAction();
    }
    void SpecialAction()
    {
        if (skillDelay > 0)
        {
            skillDelay -= Time.deltaTime;
        }
        else
        {
            skillDelay = 3f;
            isAction = true;
            var rnd = new System.Random();
            var action = rnd.Next(0, 2);
            switch (action)
            {
                case 0:
                    Skill();
                    break;
                case 1:
                    Teleport();
                    break;
            }

        }
    }
    void Skill()
    {
        state = 8;
        SetAction();
        GameObject skillClone;
        var pos = transform.position;
        for (int i = 0; i < 5; i++)
        {
            skillClone = Instantiate(skill, new Vector3(pos.x - (i + 2f), pos.y - 1f), Quaternion.Euler(new Vector3(0, 0, 0)));
            skillClone = Instantiate(skill, new Vector3(pos.x + (i + 2f), pos.y - 1f), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }

    public override void AlertObservers(string message)
    {
        base.AlertObservers(message);
        switch (message)
        {
            case "Hit":
                attack1.enabled = attack2.enabled = false;
                break;
            case "Attack1End":
                attack1.enabled = false;
                isAction = false;
                Stand();
                break;
            case "Attack2End":
                attack2.enabled = false;
                isAction = false;
                Stand();
                break;
            case "JumpEnd":
                isAction = false;
                Stand();
                break;
            case "Teleport":
                isTeleport = true;
                var newPos = new Vector2(transform.position.x + speed * teleRange, transform.position.y);
                transform.position = newPos;
                break;
            case "SkillEnd":
                isAction = false;
                Stand();
                break;
        }
    }
}

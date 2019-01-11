using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrieker : Enemy
{
    // Start is called before the first frame update
    public GameObject skill;
    public Collider2D attackLeft, attackRight;
    //private SoundManager sound;

    public bool onGround;
    
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        attackLeft.enabled = attackRight.enabled = false;
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
                if (Math.Abs(target.transform.position.x - transform.position.x) < 1f && 
                    Math.Abs(target.transform.position.y - transform.position.y) < 0.8f)
                    Attack();
                else if (Math.Abs(target.transform.position.x - transform.position.x) < 3f)
                    PrepareSkill();
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
            FallDown();
        }
    }
    void FallDown()
    {
        isInvulnerable = true;
        state = 6;
        SetAction();
        var rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0.5f;
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
    public void PrepareSkill()
    {
        if (skillDelay > 0)
            skillDelay -= Time.deltaTime;
        else
        {
            state = 5;
            skillDelay = 3f;
            SetAction();
        }
    }
    public void Skill()
    {
        GameObject skillClone;
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        if (faceRight)
        {
            skillClone = Instantiate(skill, new Vector3(transform.position.x - 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            skillClone.GetComponent<ShriekerSkill>().Initialize(direction);
        }
        else
        {
            skillClone = Instantiate(skill, new Vector3(transform.position.x + 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 180)));
            direction.Normalize();
            skillClone.GetComponent<ShriekerSkill>().Initialize(direction);
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
            case "SkillEnd":
                Skill();
                state = 0;
                SetAction();
                break;
            case "Dead":
                Destroy(gameObject);
                break;
        }
    }
}

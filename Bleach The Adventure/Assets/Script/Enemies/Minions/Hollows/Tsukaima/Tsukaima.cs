using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsukaima : Minion
{
    public GameObject skill;
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
        if (curHP > 0)
        {
            if (isAwake)
            {
                if (((target.transform.position.x < transform.position.x && !faceRight) ||
                    (target.transform.position.x > transform.position.x && faceRight)) &&
                    Math.Abs(target.transform.position.y - transform.position.y) < 1)
                    Flip();
                if (Math.Abs(target.transform.position.x - transform.position.x) < 1f)
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
        if (faceRight)
        {
            skillClone = Instantiate(skill, new Vector3(transform.position.x - 0.5f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);
            skillClone.GetComponent<TsukaimaSkill>().Initialize(Vector2.left);
        }
        else
        {
            skillClone = Instantiate(skill, new Vector3(transform.position.x + 0.5f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 180)), this.transform);
            skillClone.GetComponent<TsukaimaSkill>().Initialize(Vector2.right);
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rangiku : Enemy
{
    public GameObject skill;
    //private SoundManager sound;

    private float teleportDelay;
    private bool isTeleport;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        teleportDelay = 0f;
        isTeleport = false;
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
                    Teleport();
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

    public void Teleport()
    {
        if (teleportDelay > 0)
            teleportDelay -= Time.deltaTime;
        else
        {
            isTeleport = true;
            state = 5;
            SetAction();
            var newPos = new Vector2(transform.position.x + speed * 60f, transform.position.y);
            transform.position = newPos;
            Flip();
        }
    }
    public void PrepareSkill()
    {
        if (skillDelay > 0)
            skillDelay -= Time.deltaTime;
        else
        {
            state = 4;
            skillDelay = 2f;
            SetAction();
        }
    }
    public void Skill()
    {
        GameObject skillClone;
        if (faceRight)
        {
            skillClone = Instantiate(skill, new Vector3(transform.position.x - 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            skillClone.GetComponent<RangikuSkill>().Initialize(Vector2.left);
        }
        else
        {
            skillClone = Instantiate(skill, new Vector3(transform.position.x + 1f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 180)));
            skillClone.GetComponent<RangikuSkill>().Initialize(Vector2.right);
        }
    }

    public void AlertObservers(string message)
    {
        switch (message)
        {
            case "TeleportEnd":
                teleportDelay = 1.5f;
                isTeleport = false;
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

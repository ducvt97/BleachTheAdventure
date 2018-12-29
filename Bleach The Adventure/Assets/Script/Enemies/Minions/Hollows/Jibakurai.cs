using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jibakurai : MonoBehaviour
{
    //public Player target;
    public IchigoScript target;
    public Animator anim;
    public GameObject bullet;
    public Collider2D attackLeft, attackRight;
    //private SoundManager sound;

    public int curHP;
    public int maxHP;

    private float speed = 0.02f;
    private float awakeRange = 3f;
    private float attackDelay;
    private bool isAwake;
    private bool faceRight;

    private int state; // 0: stand; 1: walk; 2: attack; 3: take damage; 4: dead

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<IchigoScript>();
        anim = GetComponent<Animator>();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        state = 0;
        maxHP = curHP = 5;
        faceRight = false;
        isAwake = false;
        curHP = maxHP;
        attackDelay = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        isAwake = CheckRange();
        if (curHP <= 0)
            state = 4;
        else
        {
            if (isAwake)
            {
                if ((target.transform.position.x < transform.position.x && !faceRight) ||
                    (target.transform.position.x > transform.position.x && faceRight))
                    Flip();
                if (Math.Abs(target.transform.position.x - transform.position.x) < 1f)
                    Attack();
                else
                    Walk();
                
            }
            else
            {

            }
        }
    }
    void Flip()
    {
        faceRight = !faceRight;
        speed *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void SetAction()
    {
        anim.SetInteger("State", state);
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
            state = 2;
            attackDelay = 1.5f;
            if (faceRight)
                attackRight.enabled = true;
            else
                attackLeft.enabled = true;
            SetAction();
        }

    }
    public void Walk()
    {
        state = 1;
        SetAction();
        var newPos = new Vector2(transform.position.x + speed, transform.position.y);
        transform.position = newPos;
    }
    public void Stand()
    {
        state = 0;
        SetAction();
    }

    public void LoseHP(int hpLost)
    {
        curHP -= hpLost;
        state = 3;
        SetAction();
        state = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            //LoseHP(player.dmg);
        }

    }

    //public bool CheckDie()
    //{
    //    return curHP <= 0 ? true : false;
    //}

    public void AlertObservers(string message)
    {
        switch (message)
        {
            case "AttackEnd":
                attackLeft.enabled = attackRight.enabled = false;
                state = 0;
                SetAction();
                break;
            case "Dead":
                Destroy(gameObject);
                break;
        }
    }
}

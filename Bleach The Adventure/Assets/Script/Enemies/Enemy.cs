using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public IchigoScript target;
    public Animator anim;

    //private SoundManager sound;

    public int curHP;
    public int maxHP;

    protected int damage;
    protected float speed = 0.02f;
    protected float awakeRange = 5f;
    protected float attackDelay, skillDelay, walkDelay;
    protected bool isAwake;
    public bool faceRight, isInvulnerable;

    protected int state; // 0: stand; 1: walk; 2: take damage; 3: dead

    // Use this for initialization
    public virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<IchigoScript>();
        anim = GetComponent<Animator>();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        state = 0;
        maxHP = curHP = 5;
        faceRight = isAwake = isInvulnerable = false;
        attackDelay = skillDelay = 0f;
        walkDelay = 1f;
        damage = 5;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        walkDelay -= Time.deltaTime;
        if (walkDelay > 0 && walkDelay <= 1)
        {
            Stand();
        }
        else
        {
            if (walkDelay > 1)
                Walk();
            else
                walkDelay = 4f;
        }
    }

    public void Flip()
    {
        faceRight = !faceRight;
        speed *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void SetAction()
    {
        anim.SetInteger("State", state);
    }
    //public bool CheckRange()
    //{
    //    var distance = Vector2.Distance(target.transform.position, transform.position);
    //    if (distance <= awakeRange)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    public virtual void Stand()
    {
        isInvulnerable = false;
        state = 0;
        SetAction();
    }

    public virtual void Walk()
    {
        isInvulnerable = false;
        state = 1;
        SetAction();
        var newPos = new Vector2(transform.position.x + speed, transform.position.y);
        transform.position = newPos;
    }

    public virtual void LoseHP(int hpLost)
    {
        if (!isInvulnerable)
        {
            curHP -= hpLost;
            state = 2;
            SetAction();
            Stand();
        }
    }
    public void Dead()
    {
        isInvulnerable = true;
        state = 3;
        SetAction();
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isInvulnerable)
                target.TakeDamage2(damage);
        }
        if (other.CompareTag("PlayerAttack"))
        {
            LoseHP(10);
        }
        if (other.CompareTag("Edge"))
        {
            Flip();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public IchigoScript target;
    public Animator anim;

    //private SoundManager sound;

    public int curHP;
    public int maxHP;

    protected float speed = 0.02f;
    protected float awakeRange = 5f;
    protected float attackDelay, skillDelay, walkDelay;
    protected bool isAwake;
    public bool faceRight;

    protected int state; // 0: stand; 1: walk; 2: take damage; 3: dead

    // Use this for initialization
    public virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<IchigoScript>();
        anim = GetComponent<Animator>();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        state = 0;
        maxHP = curHP = 5;
        faceRight = false;
        isAwake = false;
        curHP = maxHP;
        attackDelay = skillDelay = 0f;
        walkDelay = 1f;
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
    //    var distance = Vector2.Distance(target.transform.position, this.transform.position);
    //    if (distance <= awakeRange)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    public void Stand()
    {
        state = 0;
        SetAction();
    }

    public void Walk()
    {
        state = 1;
        SetAction();
        var newPos = new Vector2(transform.position.x + speed, transform.position.y);
        transform.position = newPos;
    }

    public void LoseHP(int hpLost)
    {
        curHP -= hpLost;
        state = 2;
        SetAction();
        state = 0;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            //var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            //LoseHP(player.dmg);
        }
        if (other.CompareTag("Edge"))
        {
            Flip();
        }
    }
}

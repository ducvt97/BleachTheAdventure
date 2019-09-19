using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterOld : MonoBehaviour
{
    protected bool facingRight;     // detect the side of the character is facing
    protected bool onGround;        // detect the character is standing on ground
    protected bool isAttack;        // detect the character is attacking or using skill
    protected bool isInvulnerable;  // detect the character is being imortal (cannot cause damage or take damage)
    protected bool isDead;          // detect the character is dead (hp <= 0)
    protected float speed;          // speed of character
    protected float jumpPow;        // jump power of character
    protected int hp;               // current health point of the character
    protected int mp;               // current mana point of the character (neccessary for using skill)
    protected float attackDelay;    // time between 2 attack or skill

    public bool FacingRight {
        get { return this.facingRight; }
        set { this.facingRight = value; }
    }

    public bool OnGround
    {
        get { return this.onGround; }
        set { this.onGround = value; }
    }

    public bool IsAttack
    {
        get { return this.isAttack; }
        set { this.isAttack = value; }
    }

    public bool IsInvulnerable
    {
        get { return this.isInvulnerable; }
        set { this.isInvulnerable = value; }
    }

    public bool IsDead
    {
        get { return this.isDead; }
        set { this.isDead = value; }
    }

    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }

    public float JumpPow
    {
        get { return this.jumpPow; }
        set { this.jumpPow = value; }
    }

    public int HP
    {
        get { return this.hp; }
        set { this.hp = value; }
    }

    public int MP
    {
        get { return this.mp; }
        set { this.mp = value; }
    }

    public float AttackDelay
    {
        get { return this.attackDelay; }
        set { this.attackDelay = value; }
    }

    [SerializeField]
    protected Collider2D bodyCollider;

    protected Rigidbody2D myRigidbody;

    protected SpriteRenderer spriteRenderer;

    public CharacterOld() { }

    protected void Start()
    {
        this.myRigidbody = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected abstract void Update();

    protected abstract void FixedUpdate();

    protected abstract void OnTriggerEnter2D();

    protected void Dead()
    {
        this.isDead = true;
    }

    protected void Attack()
    {
        this.isAttack = true;
    }

    protected IEnumerator TakeDamage(int damage)
    {
        if (!this.isInvulnerable)
        {
            this.hp -= damage;
            isInvulnerable = true;
            this.bodyCollider.enabled = false;
            StartCoroutine(IndicateImmortal());
            yield return new WaitForSeconds(.3f);
            this.isInvulnerable = false;
        }
    }

    protected void Flip()
    {
        this.facingRight = !this.facingRight;
        this.speed *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void KnockBack()
    {
        var dir = facingRight ? Vector2.left : Vector2.right;
        this.myRigidbody.AddForce((Vector2.up + dir) * 130f);
    }

    public IEnumerator IndicateImmortal()
    {
        while (this.isInvulnerable)
        {
            this.spriteRenderer.enabled = false;

            yield return new WaitForSeconds(.1f);

            this.spriteRenderer.enabled = true;

            yield return new WaitForSeconds(.1f);
        }
    }
}

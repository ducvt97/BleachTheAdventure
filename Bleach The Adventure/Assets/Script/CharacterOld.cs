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

    [SerializeField]
    protected Collider2D bodyCollider;

    protected Rigidbody2D myRigidbody;

    public CharacterOld() { }

    protected void Start()
    {
        this.myRigidbody = GetComponent<Rigidbody2D>();
    }

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
        var spriteRender = GetComponent<SpriteRenderer>();
        while (this.isInvulnerable)
        {
            spriteRender.enabled = false;

            yield return new WaitForSeconds(.1f);

            spriteRender.enabled = true;

            yield return new WaitForSeconds(.1f);
        }
    }
}

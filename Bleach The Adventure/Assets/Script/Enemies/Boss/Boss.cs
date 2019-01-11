using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public bool isBlocking, isTeleport, isAction;
    public int form;
    protected float teleportDelay;
    protected float jumpPow, teleRange;

    public override void Start()
    {
        base.Start();
        //sound = GameObject.FindObjectOfType<SoundManager>();
        isBlocking = isTeleport = isAction = false;
        form = 1;
        teleportDelay = 1f;
        jumpPow = 180f;
        teleRange = 70f;
    }
    public override void LoseHP(int hpLost)
    {
        if(!isBlocking && !isTeleport)
            base.LoseHP(hpLost);
    }
    public override void Stand()
    {
        isAction = false;
        base.Stand();
    }
    public override void Walk()
    {
        isAction = false;
        base.Walk();
    }
    public void Move()
    {
        teleportDelay -= Time.deltaTime;
        if (teleportDelay < 0)
        {
            Teleport();
            teleportDelay = 2f;
        }
        else
            Walk();
    }
    public void Teleport()
    {
        isTeleport = true;
        isAction = true;
        state = 5;
        SetAction();
        var newPos = new Vector2(transform.position.x + speed * teleRange, transform.position.y);
        transform.position = newPos;
    }
    public void Block()
    {
        isBlocking = true;
        isAction = true;
        state = 4;
        SetAction();
    }
    public virtual void Jump()
    {
        var rb2d = GetComponent<Rigidbody2D>();
        var direction = faceRight ? Vector2.up + (Vector2.left + new Vector2(0.5f, 0)) :
            Vector2.up + (Vector2.right + new Vector2(-0.5f, 0));
        rb2d.AddForce(direction * jumpPow);
    }
    public void SetForm(int form)
    {
        this.form = form;
    }
    public virtual void AlertObservers(string message)
    {
        switch (message)
        {
            case "BlockEnd":
                isBlocking = false;
                isAction = false;
                Stand();
                break;
            case "TeleportEnd":
                isTeleport = false;
                isAction = false;
                state = 0;
                SetAction();
                break;
            case "Dead":
                Destroy(gameObject);
                break;
        }
    }
}

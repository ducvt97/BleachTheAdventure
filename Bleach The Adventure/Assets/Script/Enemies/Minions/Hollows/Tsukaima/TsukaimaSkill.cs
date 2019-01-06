using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsukaimaSkill : Air
{
    private Animator anim;
    private float stickElapsed, destroyElapsed;
    public int dmg = 1;

    public bool collideWithPlayer, stickOnPlayer;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        collideWithPlayer = stickOnPlayer = false;
        stickElapsed = destroyElapsed = 2f;
        speed = 1.5f;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        if (!stickOnPlayer)
        {
            if (collideWithPlayer)
            {
                stickOnPlayer = true;
                anim.SetBool("Stick", stickOnPlayer);
            }
            else
            {
                if (destroyElapsed > 0)
                {
                    destroyElapsed -= Time.deltaTime;
                    base.FixedUpdate();
                }
                else
                    Destroy(gameObject);
            }
        }
        else
        {
            if (stickElapsed > 0)
            {
                stickElapsed -= Time.deltaTime;
                var tsukaima = gameObject.GetComponentInParent<Tsukaima>();
                transform.position = tsukaima.target.transform.position;
            }
            else
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            if (other.CompareTag("Player"))
            {
                collideWithPlayer = true;
            }
        }
    }
}
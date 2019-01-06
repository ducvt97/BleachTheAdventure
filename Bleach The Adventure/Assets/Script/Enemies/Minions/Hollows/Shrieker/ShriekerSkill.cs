using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShriekerSkill : Air
{
    private Animator anim;
    //private float speed = 1.5f;
    //public Vector3 target, direction;
    public int dmg = 1;
    private float timeElapsed = 2f;
    private bool collideWithPlayer;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        collideWithPlayer = false;
        speed = 3f;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        if (timeElapsed <= 0 || collideWithPlayer)
        {
            anim.SetBool("Destroy", true);
        }
        else
        {
            timeElapsed -= Time.deltaTime;
            base.FixedUpdate();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            if (other.CompareTag("Player"))
            {
                //var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                //player.LoseHP(dmg);
                collideWithPlayer = true;
            }
        }
    }
    public void AlertObservers(string message)
    {
        if (message == "Destroy")
        {
            Destroy(gameObject);
        }
    }
}

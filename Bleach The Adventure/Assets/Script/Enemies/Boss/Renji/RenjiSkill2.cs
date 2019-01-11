using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenjiSkill2 : Air
{
    private Animator anim;
    public int dmg = 1;

    private float timeElapsed = 2f;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        speed = 5f;
    }

    // Use this for initialization
    public override void FixedUpdate()
    {
        if (timeElapsed > 0)
        {
            timeElapsed -= Time.deltaTime;
            base.FixedUpdate();
        }
        else
        {
            Destroy(gameObject);
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
                Destroy(gameObject);
            }
        }
    }
    public void AlertObservers(string message)
    {
        if (message == "Idle")
            anim.SetBool("Idle", true);

    }
}

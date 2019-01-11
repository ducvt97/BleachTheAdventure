using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YachiruSkill : Air
{
    // Start is called before the first frame update
    public int dmg = 1;

    protected float timeElapsed = 2f;

    public override void Start()
    {
        base.Start();
        speed = 4f;
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
    public void Flip()
    {
        speed *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HinamoriSkill : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public int dmg = 1;
    private float timeElapsed;
    private Hinamori parrent;
    private bool collideWithPlayer;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        parrent = gameObject.GetComponentInParent<Hinamori>();
        timeElapsed = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed > 0)
        {
            timeElapsed -= Time.deltaTime;
            Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            gameObject.GetComponent<BoxCollider2D>().size = S;
        }
        else
        {
            parrent.AlertObservers("SkillEnd");
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
                collideWithPlayer = true;
            }
        }
    }
}

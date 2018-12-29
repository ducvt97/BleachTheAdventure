using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShriekerSkill : MonoBehaviour
{
    private Animator anim;
    private float speed = 1.5f;
    public Vector3 target, direction;
    public int dmg = 1;

    private bool collideWithPlayer;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        var shrieker = gameObject.GetComponentInParent<Shrieker>();
        target = shrieker.target.transform.position;
        collideWithPlayer = false;
        direction = target - transform.position;
        if (shrieker.faceRight)
            direction.x = -direction.x;
        direction.Normalize();
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Equals(transform.position, target) || collideWithPlayer)
        {
            anim.SetBool("Destroy", true);
        }
        else
        {

            float factor = Time.deltaTime * speed;

            this.transform.Translate(direction * factor, Space.Self);
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

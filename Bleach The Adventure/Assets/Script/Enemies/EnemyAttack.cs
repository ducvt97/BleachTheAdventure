using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int dmg;
    IchigoScript target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<IchigoScript>();
        dmg = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(target.TakeDamage2(dmg));
        }
    }
}

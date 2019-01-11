using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenosGrandeSkill : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            //LoseHP(3);
        }
    }
    public void AlertObservers(string message)
    {
        if (message == "Destroy")
            Destroy(gameObject);
    }
}

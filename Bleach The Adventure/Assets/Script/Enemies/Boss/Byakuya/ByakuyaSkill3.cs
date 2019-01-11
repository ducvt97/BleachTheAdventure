using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByakuyaSkill3 : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            if (other.CompareTag("Player"))
            {
                var player = FindObjectOfType<IchigoScript>();
                player.TakeDamage();
                Destroy(gameObject);
            }
        }
    }
}

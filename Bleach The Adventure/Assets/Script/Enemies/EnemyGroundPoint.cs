using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponents<Collider2D>();
        for(int i = 0; i < player.Length;i++)
        {
            if (!player[i].isTrigger)
                Physics2D.IgnoreCollision(player[i], GetComponent<Collider2D>());
        }
    }
}

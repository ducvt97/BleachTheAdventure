using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonnieSight : MonoBehaviour
{
    [SerializeField]
    private Bonnie enermy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enermy.Target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enermy.Target = null;
        }
    }
}

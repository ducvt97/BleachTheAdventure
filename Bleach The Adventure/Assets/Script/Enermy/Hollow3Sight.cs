using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hollow3Sight : MonoBehaviour
{
    [SerializeField]
    private Hollow3 enermy;

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

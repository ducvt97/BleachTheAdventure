using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    public GameObject follow;

    public virtual void Start()
    {
        
    }
    public virtual void FixedUpdate()
    {
        transform.position = follow.transform.position;
    }
    public virtual void Initialize(GameObject obj)
    {
        follow = obj;
    }
}

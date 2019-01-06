using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Air : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    protected Rigidbody2D myRigidbody;

    protected Vector2 direction;
    // Start is called before the first frame update
    public virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public virtual void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

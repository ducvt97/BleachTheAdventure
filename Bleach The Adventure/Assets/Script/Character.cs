using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    

    [SerializeField]
    protected Transform airPos;

    [SerializeField]
    protected float movementSpeed;

    protected bool facingRight;

    [SerializeField]
    private GameObject airPrefab;

    [SerializeField]
    protected int health;

    [SerializeField]
    private EdgeCollider2D swordCollider;

    [SerializeField]
    private EdgeCollider2D swordCollider2;

    [SerializeField]
    private List<string> damageSources;

    public abstract bool IsDead { get; }

    public bool Attack { get; set; }

    public bool TakingDamage { get; set; }

    public Animator MyAnimator { get; private set; }

    public EdgeCollider2D SwordCollider
    {
        get
        {
            return swordCollider;
        }
    }

    public EdgeCollider2D SwordCollider2
    {
        get
        {
            return swordCollider2;
        }
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        facingRight = true;

        MyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract IEnumerator TakeDamage();

    public abstract void Death();

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public virtual void ThrowAir(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(airPrefab, airPos.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            tmp.GetComponent<Air>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(airPrefab, airPos.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            tmp.GetComponent<Air>().Initialize(Vector2.left);
        }
    }

    public void MeleeAttack()
    {
        SwordCollider.enabled = true;
    }

    public void MeleeAttack2()
    {
        swordCollider2.enabled = !swordCollider2.enabled;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag)) 
        {
            StartCoroutine(TakeDamage());
        }
    }
}

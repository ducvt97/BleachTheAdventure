using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DeadEventHandler();

public class IchigoScript : Character
{
    private static IchigoScript instance;

    public event DeadEventHandler Dead;

    public static IchigoScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<IchigoScript>();
            }
            return instance;
        }
    }
    private float tapSpeed = 0.5f;

    private float lastTapTime = 0;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    private bool immortal = false;

    private SpriteRenderer spriteRender;

    [SerializeField]
    private float immortalTime;

    public Rigidbody2D MyRigidbody { get; set; }

    public bool Attack2 { get; set; }

    public bool Jump { get; set; }

    public bool Form2 { get; set; }

    public bool OnGround { get; set; }
    [SerializeField]
    private Stat statHealth;
    [SerializeField]
    private Stat statEnergy;

    public AudioSource JumpSound;

    public AudioSource Attack1Sound;

    public AudioSource Attack2Sound;

    public AudioSource AttackAirSound;

    public AudioSource TakeDamageSound;

    public AudioSource DeathSound;

    public override bool IsDead
    {
        get
        {
            if (health <= 0)
            {
                OnDead();
            }
            
            return health <= 0;
        }
    }

    private Vector2 startPos;

    public List<Collider2D> attackColliders;

    // Start is called before the first frame update
    private void Awake()
    {
        statHealth.Init();
        statEnergy.Init();
    }
    public override void Start()
    {
        base.Start();
        startPos = transform.position;
        spriteRender = GetComponent<SpriteRenderer>();
        MyRigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -14f)
            {
                Death();
            }
            HandleInput();
        }
        
    }

    void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");

            OnGround = IsGrounded();

            HandleMovement(horizontal);

            Flip(horizontal);

            HandleLayers();
        }
        

    }
    
    public void OnDead()
    {
        if (Dead != null)
        {
            Dead();
        }
    }

    private void HandleMovement(float horizontal)
    {
        if(MyRigidbody.velocity.y < 0)
        {
            MyAnimator.SetBool("land", true);
        }
        if(!Attack &&!Attack2&&(OnGround || airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }
        if(Jump && MyRigidbody.velocity.y == 0)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
        }

        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    
    // call attack animation


    //input keycode
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("jump");
            JumpSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MyAnimator.SetTrigger("attack_stand1");
            Attack1Sound.Play();
            if ((Time.time - lastTapTime) < tapSpeed)
            {
                MyAnimator.SetTrigger("attack_stand2");
                Attack2Sound.Play();
            }
            lastTapTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            MyAnimator.SetTrigger("attack_air");
            AttackAirSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //MyAnimator.SetTrigger("attack_air");
            //AttackAirSound.Play();
            if(this.statEnergy.CurrentVal >= 100)
            {

            }
        }
    }

    //reset value

    //xoay nguoi player
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }
    // stand in ground?
    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    } 
                }
            }

        }
        return false;
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }
 
    public override void ThrowAir(int value)
    {
        if (!OnGround && value == 1 || OnGround && value == 0)
        {
            base.ThrowAir(value);
        }
        
    }

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRender.enabled = false;

            yield return new WaitForSeconds(.1f);

            spriteRender.enabled = true;

            yield return new WaitForSeconds(.1f);
        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            KnockBack();

            statHealth.CurrentVal -= 10;
            health = (int)statHealth.CurrentVal;

            if (!IsDead)
            {
                MyAnimator.SetTrigger("damage");
                TakeDamageSound.Play();
                immortal = true;

                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
                DeathSound.Play();
            }
        }
    }
    public IEnumerator TakeDamage2(int hpLost)
    {
        if (!immortal)
        {
            KnockBack();

            statHealth.CurrentVal -= hpLost;
            health = (int)statHealth.CurrentVal;

            if (!IsDead)
            {
                MyAnimator.SetTrigger("damage");
                TakeDamageSound.Play();
                immortal = true;

                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
                DeathSound.Play();
            }
        }
    }
    public override void Death()
    {
        MyRigidbody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("stand");
        health = (int)statHealth.MaxVal;
        statHealth.CurrentVal = statHealth.MaxVal;
        transform.position = startPos;
    }
    public void KnockBack()
    {
        var dir = facingRight ? Vector2.left : Vector2.right;
        MyRigidbody.AddForce((Vector2.up + dir) * 130f);
    }
}

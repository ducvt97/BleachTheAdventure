using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterOld
{
    protected float attackComboInterruptedTime;
    protected float lastTapAttackTime;
    protected float lastTapTeleportTime;

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

    public Player() : base() { }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.facingRight = true;
        this.onGround = false;
        this.isAttack = false;
        this.isInvulnerable = true;
        this.isPlayingAction = false;
        this.isDead = false;

        this.attackComboInterruptedTime = 0.5f;
        this.lastTapAttackTime = 0f;
        this.lastTapTeleportTime = 0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void FixedUpdate()
    {
        
    }

    //input keycode
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.animator.SetTrigger("attack_stand1");
            Attack1Sound.Play();
            if ((Time.time - lastTapTeleportTime) > this.teleportDelay)
            {
                this.animator.SetTrigger("attack_stand2");
                Attack2Sound.Play();
            }
            lastTapAttackTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            this.animator.SetTrigger("attack_air");
            AttackAirSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //MyAnimator.SetTrigger("attack_air");
            //AttackAirSound.Play();
            if (this.statEnergy.CurrentVal >= 100)
            {

            }
        }
    }

    protected void Jump()
    {
        this.animator.SetTrigger("jump");
        JumpSound.Play();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        throw new System.NotImplementedException();
    }
}

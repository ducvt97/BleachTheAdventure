using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask Ground;

    private bool attack;
    private bool dash;
    private bool jump;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpForce;
    private bool running;
    private bool isGrounded;
    private bool facingRight;
    private float tapSpeed = 0.5f;
    private float lastTapTime = 0;
    private float DashTime = 0;
    // Use this for initialization
    void Start () {
        facingRight = true;
        running = false;
        dash = false;
        lastTapTime = 0;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        

	}
	
	// Update is called once per frame
    void Update()
    {
        HandleInput();
    }

	void FixedUpdate ()
	{
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();
		HandleMovement(horizontal);
        HandleRun();
        Flip(horizontal);
        HandleDash(horizontal);
        HandleAttacks();
        HandleLayer();
        ResetValues();
    }

	private void HandleMovement(float horizontal)
	{
        if (myRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }
        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
        }
        if (!myAnimator.GetBool("isDash") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
        {
            if (this.myAnimator.GetBool("isRunning"))
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed*2, myRigidbody.velocity.y);
            else
                myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        
        if (dash && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {
            myAnimator.SetBool("isDash", true);
        }
        else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {
            myAnimator.SetBool("isDash", false);
        }
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

	}

    private void HandleRun()
    {
        if (running)
        {
            myAnimator.SetBool("isRunning", true);
        }
       
    }


    private void HandleDash(float horizontal)
    {
        if ((Time.time - DashTime) > tapSpeed)
        {
            DashTime = 0;
            dash = false;
            myAnimator.SetBool("isDash", false);
        }
        if (dash && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {
            myRigidbody.velocity = Vector2.zero;
            //myRigidbody.MovePosition(myRigidbody.position + new Vector2(horizontal*20,0));
            if (facingRight)
                    myRigidbody.velocity = new Vector2(12, myRigidbody.velocity.y);
                    //myRigidbody.MovePosition(myRigidbody.position + new Vector2(-5,0));
            else
                    myRigidbody.velocity = new Vector2(-12, myRigidbody.velocity.y);
            dash = false;
        }

    }

    private void HandleAttacks()
    {
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnimator.SetTrigger("attack");
            myRigidbody.velocity = Vector2.zero;
        }
    }


    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            attack = true;
            running = false;
            myAnimator.SetBool("isRunning", false);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            running = false;
            myAnimator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if ((Time.time - lastTapTime) < tapSpeed)
            {
                running = true;
            }
            lastTapTime = Time.time;
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            dash = true;
            DashTime = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            dash = false;
            DashTime = 0;
            myAnimator.SetBool("isDash", false);
        }


    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !myAnimator.GetBool("isDash"))
            {
                facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
    }

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, Ground);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");
                        myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayer()
    {
        if (!isGrounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    private void ResetValues()
    {
        attack = false;
        running = false;
        dash = false;
        jump = false;
    }

}

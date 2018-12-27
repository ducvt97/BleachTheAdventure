using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchigoScript : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    private Animator myAnimator;

    private bool attack;

    private bool attack2;

    private float tapSpeed = 0.5f;

    private float lastTapTime = 0;

    [SerializeField]
    private float movementSpeed;

    private bool facingRight;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleAttack();

        HandleLayers();

        ResetValue();
    }
    
    private void HandleMovement(float horizontal)
    {
        if (myRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack") && (!isGrounded || airControl))
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }

        if(isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
        }
        
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    
    // call attack animation
    private void HandleAttack()
    {
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack")) 
        {
            myAnimator.SetTrigger("attack_stand1");
            myRigidbody.velocity = Vector2.zero;
        }
        if (attack2 && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack")) 
        {
            myAnimator.SetTrigger("attack_stand2");
            myRigidbody.velocity = Vector2.zero;
        }
    }

    //input keycode
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            attack = true;
            if ((Time.time - lastTapTime) < tapSpeed)
            {
                attack2 = true;
            }
            lastTapTime = Time.time;
        }
        
    }

    //reset value
    private void ResetValue()
    {
        attack = false;
        attack2 = false;
        jump = false;
    }
    //xoay nguoi player
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    // stand in ground?
    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

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

    private void HandleLayers()
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
}

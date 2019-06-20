using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Rigidbody2D myRigidbody;
    [SerializeField] float runSpeed=5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] float climbSpeed = 5f;
    //[SerializeField] public VirtualJoystic moveJoystick;
    [SerializeField] Joystick joystick;
    public bool isCanvasOpened;
    bool isAlive = true;
    Animator myAnimator;
    Collider2D myBodyCollider;
    BoxCollider2D feetCollider;
    float gravityScaleAtStart;
    float originalJumpSpeed;
	// Use this for initialization
	void Start () {

        originalJumpSpeed = jumpSpeed;
        isCanvasOpened = false;
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<Collider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isAlive || isCanvasOpened) { return; }
        Run3();
        //Run();
        FlipSprite();
        Jump();
        JumpSpeed();
        //ClimbLadder();
        ClimbLadder3();
        Die();
		
	}

    void JumpSpeed()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Box")))
        {
            jumpSpeed = originalJumpSpeed * 2;
        }

        else
        {
            jumpSpeed = originalJumpSpeed;
        }

    }

    /*void GetDirection()
    {
        //direction = "";

        //    print(Input.GetAxis(Axis_Y));

        if (moveJoystick.Vertical() > 0) // Just Calling Method from         //VirtualJouystic script
        {
            ClimbLadder2("up");
        }
        if (moveJoystick.Vertical2() < 0)
        {
            ClimbLadder2("down");

        }
        if ((moveJoystick.Vertical() == 0) && (moveJoystick.Vertical2() == 0))
        {
            ClimbLadder2("");
        }
        if (moveJoystick.Horizontal() > 0)
        {
            Run2("right");

        }
        if (moveJoystick.Horizontal2() < 0)
        {
            Run2("left");
        }
        if ((moveJoystick.Horizontal() == 0) && (moveJoystick.Horizontal2() == 0))
        {
            Run2("");
        }


        
        
         

    }*/

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards")) || feetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }

    }

    private void Run2(string movement)
    {
        float controlThrow = 0;
        if (movement == "left")
            controlThrow = -1;
        if (movement == "right")
            controlThrow = 1;
        if (movement == "")
        {
            controlThrow = 0;
        }
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool PlayerHasSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasSpeed)
        {
            myAnimator.SetBool("Running", true);
        }
        else
        {
            myAnimator.SetBool("Running", false);
        }

    }
    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow* runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool PlayerHasSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasSpeed)
        {
            myAnimator.SetBool("Running", true);
        }
        else
        {
            myAnimator.SetBool("Running", false);
       }
    }

    private void Run3()
    {
        if (!isAlive || isCanvasOpened) { return; }
        float controlThrow = joystick.Horizontal;
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool PlayerHasSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        bool isInAir = !feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Box"));
        if (PlayerHasSpeed && !isInAir)
        {
            myAnimator.SetBool("Running", true);
        }
        else
        {
            myAnimator.SetBool("Running", false);
        }
    }
    private void Jump()
    {
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Box")))
        {
            //Debug.Log("Not touvhing");
            return;
        }


        if(Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    public void Jump2()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground","Box")))
        {
            Debug.Log("Not touvhing");
            return;
        }


        
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        
    }
    private void ClimbLadder()
    {
        if(!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale=gravityScaleAtStart;
            myAnimator.SetBool("Climbing", false);
            return;
        }
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;
        bool PlayerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", PlayerHasVerticalSpeed);

    }

    private void ClimbLadder3()
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("Climbing", false);
            return;
        }
        float controlThrow = joystick.Vertical;
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;
        bool PlayerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", PlayerHasVerticalSpeed);

    }

    private void ClimbLadder2(string movement)
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("Climbing", false);
            return;
        }
        float controlThrow = 0;
        if (movement == "left")
            controlThrow = -1;
        if (movement == "right")
            controlThrow = 1;
        if (movement == "")
        {
            controlThrow = 0;
        }
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;
        bool PlayerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", PlayerHasVerticalSpeed);

    }

    private void FlipSprite()
    {
        bool PlayerHasSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}

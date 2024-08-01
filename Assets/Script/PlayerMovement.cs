using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;

    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float wallSlideSpeed = 5;
    public float wallJumpLerp = 10;

    [Header("Booleans")]
    public bool canMove = true;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;

    private bool canDoubleJump;
    private int jumpsRemaining = 1;

    private Coroutine powerUpCoroutine;

    public int side = 1;

    

    

    //private int jumpsRemainig =1;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);
        
        Walk(dir);
        FlipSprite(x);

        if (coll.onWall && Input.GetButton("Fire3") && canMove)
        {
            if(side != coll.wallSide)
                side *= -1;
            wallGrab = true;
            wallSlide = false;
        }

        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (coll.onGround)
        {
            wallJumped = false;
            jumpsRemaining = 1;
        }
        
        if (wallGrab)
        {
            rb.gravityScale = 0;
            if(x > .2f || x < -.2f)
                rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;
            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }
        else
        {
            rb.gravityScale = 3;
        }

        if(coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (Input.GetButtonDown("Jump"))
        {
           if (coll.onGround)
            {
                Jump(Vector2.up);
                jumpsRemaining = canDoubleJump ? 1 : 0;
            }
            else if (canDoubleJump && jumpsRemaining > 0)
            {
                Jump(Vector2.up);
                jumpsRemaining--;
            }
            else if (coll.onWall && !coll.onGround)
            {
                WallJump();
            }
        }

        if (x > 0)
        {
            side = 1;
        }
        if (x < 0)
        {
            side = -1;
        }

       
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;
        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    private void WallSlide()
    {
        if(coll.wallSide != side)
         side *= -1;

        if (!canMove)
            return;

        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -wallSlideSpeed);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir, bool wall = false)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

          // Activa el trigger de salto en el Animator
        
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    public void ActivateDoubleJump(float duration)
    {
          if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }
        powerUpCoroutine = StartCoroutine(DoubleJumpPowerUp(duration));
    }

     private IEnumerator DoubleJumpPowerUp(float duration)
    {
        canDoubleJump = true;
        jumpsRemaining = 2;

        yield return new WaitForSeconds(duration);

        canDoubleJump = false;
        jumpsRemaining = 1;
        powerUpCoroutine = null;
    }

    private void FlipSprite(float x)
    {
        if (x != 0)
        {
            // Si x es positivo, flipX debe ser false (mirando a la derecha)
            // Si x es negativo, flipX debe ser true (mirando a la izquierda)
            spriteRenderer.flipX = (x < 0);
        }
    }











}

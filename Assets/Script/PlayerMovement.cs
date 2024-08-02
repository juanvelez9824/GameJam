using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    AudioManager audiomanager;

    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    
    [Header("Booleans")]
    public bool canMove = true;
    private bool canDoubleJump;
    private int jumpsRemaining = 1;

    private Coroutine powerUpCoroutine;

    public int side = 1;

    

    [SerializeField] private ParticleSystem particulas;

    
    private void Awake() {
        
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    

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

        

        if (coll.onGround)
        {
        
            jumpsRemaining = 1;
        }
        
      


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



    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;
            
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        
    }

    private void Jump(Vector2 dir, bool wall = false)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        particulas.Play();
        audiomanager.PlaySFX(audiomanager.Jump);
         
        
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

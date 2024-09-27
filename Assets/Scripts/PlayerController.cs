using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    public Animator anim;
    public Rigidbody2D rb;
    private float moveX = 0f;
    public float velocityIncrement = .05f;

    public float inAirVelocityDelta = .5f;
    private float currentVelocityDelta = 1f;

    // Variables for movement stuffs
    public float playerSpeed;

    // Variables for jumping stuffs
    public float jumpForce;
    private bool isOnGround;
    public float positionRadius;
    public LayerMask ground;
    public Transform playerPos;
    private bool doJump = false;
    public float jumpCooldown = 1f;
    private float jumpTimer;
    public float gravityScale = 5f;
    public float fallingGravityScale = 10f;

    public Rigidbody2D bodyRB;

    // Variables for jump particle stuffs
    public ParticleSystem dust;
    public Transform leftLeg;
    public Transform rightLeg;
    public float dustOffset;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] colliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int k = i + 1; k < colliders.Length; k++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);
        if (!isOnGround)
        {
            currentVelocityDelta = inAirVelocityDelta;
        }
        else
        {
            currentVelocityDelta = 1f;
        }

        if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            doJump = true;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                //rb.AddForce(Vector2.right * playerSpeed * Time.deltaTime);
                moveX += velocityIncrement * currentVelocityDelta;
                if (moveX >= 1f)
                {
                    moveX = 1f;
                }
            }
            else
            {
                // rb.AddForce(Vector2.left * playerSpeed * Time.deltaTime);
                moveX -= velocityIncrement * currentVelocityDelta;
                if (moveX <= -1f)
                {
                    moveX = -1f;
                }
            }

            if (!isOnGround)
            {
                if (moveX > 0.1)
                {
                    anim.Play("jump");
                }
                else if (moveX < -0.1)
                {
                    anim.Play("jumpBack");
                }
                else
                {
                    anim.Play("jumpIdle");
                }
            }
            else if (moveX < 0)
            {
                anim.Play("walkBack");
            }
            else
            {
                anim.Play("walk");
            }
        }
        else
        {
            if (moveX != 0)
            {
                if (moveX < 0)
                {
                    moveX += velocityIncrement * currentVelocityDelta;
                }
                else
                {
                    moveX -= velocityIncrement * currentVelocityDelta;
                }
            }

            if (isOnGround)
            {
                anim.Play("idle");
            }
            else if (moveX > 0.1)
            {
                anim.Play("jump");
            }
            else if (moveX < -0.1)
            {
                anim.Play("jumpBack");
            }
            else
            {
                anim.Play("jumpIdle");
            }
        }
    }

    void FixedUpdate()
    {  
        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);

        if (!isOnGround || !doJump)
        {
            rb.velocity = new Vector3(moveX * playerSpeed, rb.velocity.y); 
        }
        else
        {
            rb.velocity = new Vector3(moveX * playerSpeed, 0f);
        }
 
        jumpTimer += Time.fixedDeltaTime;

        if (!isOnGround)
        {
            jumpTimer = jumpCooldown;
        }

        if (doJump)
        {
            if (jumpTimer >= jumpCooldown)
            {
                rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
                bodyRB.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
                //CreateDust();
                jumpTimer = 0f;
            }
            doJump = false;
        }
        if (!isOnGround)
        {
            if (rb.velocity.y >= 0)
            {
                rb.gravityScale = gravityScale;
            }
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = fallingGravityScale;
            }
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    void CreateDust()
    {
        float newY = Mathf.Min(leftLeg.position.y, rightLeg.position.y) + dustOffset;
        dust.transform.position = new Vector3(dust.transform.position.x, newY, dust.transform.position.z);
        dust.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Animator anim;
    public BoxCollider2D feetCol;

    public float dirx = 0f;
    public float horizontalMove = 0f;
    public float speedx = 7f;
    private int jumpCount = 0;
    public bool running;
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        feetCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        dirx = Input.GetAxisRaw("Horizontal");
        horizontalMove = dirx * speedx;

        rb.velocity = new Vector2(dirx * speedx, rb.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (grounded)
        {
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("isJumping", true);
            if (jumpCount < 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, 14f);
                //jumpCount++;
            }


        }

        UpdateAnimationUpdate();

    }

    public void OnLanding ()
    {
        anim.SetBool("isJumping", false);
    }

    public void UpdateAnimationUpdate()
    {
        if (dirx > 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        else if (dirx < 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = false;
        }
        else
        {
            anim.SetBool("running", false);
        }
    }
}

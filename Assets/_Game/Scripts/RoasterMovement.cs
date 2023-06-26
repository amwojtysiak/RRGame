using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoasterMovement : MonoBehaviour
{

    public TestMovement controller;
    public Animator anim;
    public SpriteRenderer sprite;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("isJumping", true);
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        //UpdateAnimationUpdate();

    }

    void FixedUpdate()
    {
        // Move our character 
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }

    public void OnLanding()
    {
        Debug.Log("Landing");
        anim.SetBool("isJumping", false);
    }

    public void OnCrouching(bool isCrouching) 
    {
        anim.SetBool("isCrouching", isCrouching);
    }

    public void UpdateAnimationUpdate()
    {
        if (horizontalMove > 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        else if (horizontalMove < 0f)
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

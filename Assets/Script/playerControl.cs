using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    public Rigidbody2D rb;      //get Rigifbody2d
    public Animator anim;       //get Animation
    public Collider2D coll;     //get Collider2d
    public LayerMask ground;    //get ground layer

    [Range(0, 1000)] [SerializeField] public float speed = 0f;         //init horizontal movement speed
    [Range(0, 1000)] [SerializeField] public float jumpForce = 0f;     //init jumping speed

    float moveH, faceDir = 0f;
    bool isJump = false;


    /*Update is called once per frame*/

    void Update()
    {
        moveH = Input.GetAxis("Horizontal");//L:-1; R:1
        faceDir = Input.GetAxisRaw("Horizontal");
        AnimationRendering();
    }



    void FixedUpdate()
    {
        OnMovement();
        if (isJump)
        {
            Debug.Log("fixed " + isJump);
            OnJumping();
        }
        OnFacing();
    }

    /*handle player horizontal movements*/
    void OnMovement()
    {
        rb.velocity = new Vector2(moveH * speed * Time.deltaTime, rb.velocity.y);
    }

    /*handle player jumping*/
    void OnJumping()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
        isJump = false;
    }

    /*handle player facing direction*/
    void OnFacing()
    {
        if (faceDir != 0)
        {
            transform.localScale = new Vector3(faceDir, 1, 1);
        }
    }

    /*handle player animation states*/
    void AnimationRendering()
    {
        /*running or idle*/
        anim.SetFloat("RunningSpeed", Mathf.Abs(faceDir));
        /*start jumping*/
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            anim.SetBool("IsJumping", true);
            isJump = true;
        }

        /*start falling*/
        if (anim.GetBool("IsJumping") && rb.velocity.y < 0)
        {
            Debug.Log("anim1 " + isJump);

            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", true);
            isJump = false;
        }
        /*back on ground*/
        else if (anim.GetBool("IsFalling") && coll.IsTouchingLayers(ground))
        {
            anim.SetBool("IsFalling", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    public Rigidbody2D rb;      //get Rigifbody2d
    public Animator anim;       //get Animation
    public Collider2D coll;     //get Collider2d
    public LayerMask ground;    //get ground layer

    public float speed;         //init horizontal movement speed
    public float jumpForce;     //init jumping speed


    /*Start is called before the first frame update*/
    void Start()
    {

    }

    /*Update is called once per frame*/
    void FixedUpdate()
    {
        Movement();
        SwitchAnimation();
    }

    void Movement()
    {
        float moveH = Input.GetAxis("Horizontal");//L:-1; R:1
        float faceDir = Input.GetAxisRaw("Horizontal");
        // Debug.Log(moveH);

        /*player horizontal movements*/
        if (moveH != 0)
        {
            rb.velocity = new Vector2(moveH * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(faceDir));
        }
        /*player facing*/
        if (faceDir != 0)
        {
            transform.localScale = new Vector3(faceDir, 1, 1);
        }
        /*player jump*/
        if (Input.GetButton("Jump"))
        {
            Debug.Log(jumpForce * Time.deltaTime);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            /*change state to jumping*/
            anim.SetBool("jumping", true);
        }

    }


    void SwitchAnimation()
    {

        anim.SetBool("idle", false);
        /*jumping state => falling state*/
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        /*falling state => idle*/
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }
}

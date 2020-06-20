using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;

    // Start is called before the first frame update
    void Start() {
    
    }

    // Update is called once per frame
    void Update() {
        Movement();
    }

    void Movement() {
        float moveH= Input.GetAxis("Horizontal");//L:-1; R:1
        Debug.Log("Hello: " + moveH);
        if (moveH != 0) {
            rb.velocity = new Vector2(moveH * speed, rb.velocity.y);
        }
    }

}

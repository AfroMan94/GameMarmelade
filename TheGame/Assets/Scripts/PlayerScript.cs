using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    Rigidbody2D rb;

    public bool isJumping;
    public bool isGrounded;

    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    bool startTouch = false;

    public float jumpForce;
    public float speed;

    float touchStartTime = 0f;

    public float jumpTime;
    private float jumpTimeCounter;

    void Start () {

        isJumping = false;
        rb = this.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {


		if (Input.touchCount > 0)
        {

            if (isGrounded==true&& isJumping == false)
            {
                Debug.Log("tapped");
                TapJump();
            }
            if (isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    TapHoldJump();
                }
                else
                {
                    isJumping = false;
                }
            }
        }
        else if(isGrounded==false && isJumping==true && Input.touchCount<=0)
        {
            jumpTimeCounter = 0;
        }

    }

    void TapJump()
    {
        jumpTimeCounter = jumpTime;
        rb.velocity = Vector2.up * jumpForce;
        isJumping = true;
    }

    void TapHoldJump()
    {
        rb.velocity = Vector2.up * jumpForce;
        jumpTimeCounter -= Time.deltaTime;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position,checkRadius,whatIsGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    Rigidbody2D rb;

    public bool isJumping;
    public bool isGrounded;
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging = false;

    private Vector2 startTouch, swipeDelta;

    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float jumpForce;
    public float speed;

    float touchStartTime = 0f;

    public float jumpTime;
    private float jumpTimeCounter;

    void Start () {

        isJumping = false;
        rb = this.GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown=false;
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

    void FixedUpdate () {

		if (Input.touchCount > 0)
        {
            //Checking tap phases
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase==TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }

            //Swipe distance calculations
            swipeDelta = Vector2.zero;
            if (isDragging)
            {
                if (Input.touches.Length > 0)
                {
                    swipeDelta = Input.touches[0].position - startTouch;
                    
                }
            }

            if (swipeDelta.magnitude > 100)
            {
                //Which direction?
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if(Mathf.Abs(x)>Mathf.Abs(y))
                {
                    //Left or right
                    if (x < 0)
                    {
                        swipeLeft = true;
                        Debug.Log("left");
                    }
                    else
                    {
                        swipeRight = true;
                        Debug.Log("right");
                        GameObject.Find("mainObject").GetComponent<PublicValuesHandler>().swipeRight = true;
                    }
                }
                else
                {
                    //Up or down
                    if (y < 0)
                    {
                        swipeDown = true;
                        Debug.Log("down");

                    }
                    else
                    {
                        swipeUp = true;
                        Debug.Log("up");
                    }
                }

                Reset();
            }


            //Jumping logic
            if (isGrounded==true&& isJumping == false)
            {
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

    private void Reset()
    {
        startTouch = swipeDelta=  Vector2.zero;
        isDragging = false;
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
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    

}

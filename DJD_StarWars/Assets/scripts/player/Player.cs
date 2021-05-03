using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    [SerializeField]
    private float       jumpSpeed = 100;
    [SerializeField]
    private float       maxJumpTime = 0.1f;
    [SerializeField]
    private int         maxJumps = 1;  
    [SerializeField]
    private int         jumpGravityStart = 1;  

    private float       hAxis;
    private int         nJumps;
    private float       timeOfJump;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(hAxis * moveSpeed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxis("Horizontal");

        if((isGround()) && (Mathf.Abs(rb.velocity.y) < 0.1f))
        {
            nJumps = maxJumps;
        }

        if(Input.GetButtonDown("Jump") && nJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            nJumps -= 1;

            rb.gravityScale = jumpGravityStart; 
            
            timeOfJump = Time.time; 
        }
        else
        {
            float elapsedTimeSinceJump = Time.time - timeOfJump;

            if(Input.GetButton("Jump") && (elapsedTimeSinceJump < maxJumpTime))
            {
                rb.gravityScale = jumpGravityStart;
            }
            else
            {
                rb.gravityScale = 4.0f;
            }
        }

        if(rb.velocity.x < -0.1f)
        {
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.y = 180;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
        else if(rb.velocity.x > 0.1f)
        {
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.y = 0;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}

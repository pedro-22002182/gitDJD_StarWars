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


    //Atack Variables
    [SerializeField]
    private Transform       atackPos;
    [SerializeField]
    private float       radiusAtack;

    private bool isAtack;

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


        if(Input.GetMouseButtonDown(0))
        {
           // if(Input.GetKeyDown("m"))
           // {
           //     atackPos.position = new Vector3(0, atackPos.position.x, atackPos.position.z);
           // }

            Collider2D[] cols = Physics2D.OverlapCircleAll(atackPos.position, radiusAtack);
            CheckCircle(cols);
            isAtack = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isAtack = false;
        }


        //FLIP Player
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

    private void CheckCircle(Collider2D[] cols)
    {
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.CompareTag("enemy"))
            {
                col.gameObject.GetComponent<Character>().takeDamage(dano);
            }
        }
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        if(isAtack)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(atackPos.position, radiusAtack);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

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
    private Transform       atackFrontPos;
    [SerializeField]
    private Transform       atackUpPos;
    [SerializeField]
    private float       radiusAtack;
    private Transform       atackPos;


    //MANA COntrol
    private ManaUiPlayer manaBar;
    [SerializeField]
    private int maxMana;
    public int CurrentMana {get; set;}


    private bool isAtack;

    private float       hAxis;
    private int         nJumps;
    private float       timeOfJump;


    //RASTEIRA
    float maxSpeed;
    bool rasteira;
    [SerializeField]
    private float boostRasteira;

    [SerializeField]
    private GameObject espada;

    private GameObject arma;

    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        manaBar = FindObjectOfType<ManaUiPlayer>(); 

        CurrentMana = maxMana;
        manaBar.SetMaxMana(maxMana);

        maxSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(hAxis * moveSpeed, rb.velocity.y);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
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
                rb.gravityScale = 2.5f;
            }
        }
        

        if(Input.GetKey(KeyCode.W))
        {
            atackPos = atackUpPos;
        }
        else
        {
            atackPos = atackFrontPos;
        }

        

        //RASTEIRA
        if(Input.GetKeyDown("s"))
        {
            rasteira = true;
            moveSpeed = maxSpeed + boostRasteira;

            transform.rotation =  Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y , 90);
            
        }
        if(Input.GetKeyUp("s"))
        {
            rasteira = false;
            moveSpeed = maxSpeed;

            transform.position = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);
            transform.rotation =  Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y , 0);
        }

        if(rasteira)
        {
            if(moveSpeed > 0)
            {
                moveSpeed -= 0.6f;
            }
            else
            {
                moveSpeed = 0;
            }            
        }


        

        //ATAQUE
        if(Input.GetMouseButtonDown(0))
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(atackPos.position, radiusAtack);
            CheckCircle(cols);
            isAtack = true;
            arma = Instantiate(espada, atackPos.position , atackPos.rotation);
        }

        if(Input.GetMouseButton(0))
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(atackPos.position, radiusAtack);
            CheckCircle(cols);
            isAtack = true;
            arma.transform.position = atackPos.position;
            arma.transform.rotation = atackPos.rotation;
        }

        if(Input.GetMouseButtonUp(0))
        {
            isAtack = false;
            Destroy(arma.gameObject);
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

            if (col.gameObject.CompareTag("bullet"))
            {   
                if(rb.velocity.x > -0.4f && rb.velocity.x < 0.4f)
                {
                    col.gameObject.GetComponent<Bullet>().Ricochete();
                }
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

    public bool isMove()
    {
        if(rb.velocity.x > -0.1f && rb.velocity.x < 0.1f)
        {
            return false;
        }

        return true;
    }

    protected override void onDeath()
    {
        base.onDeath();
        SceneManager.LoadScene("menu");
    }
    
}

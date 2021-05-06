﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    protected int vidaMAx = 3;

    [SerializeField]
    protected int dano = 1;

    [SerializeField]
    protected float  invulnerabilityDuration = 2f;
    [SerializeField]
    protected float  blinkerDuration = 0.15f;

    [SerializeField]
    protected float       moveSpeed = 150;

    [SerializeField]
    protected Transform   groundCheckObject;
    [SerializeField]
    private Transform groundCheckFront;
    [SerializeField]
    private float       groundCheckRadius = 3.0f;
    [SerializeField]
    protected LayerMask   groundCheckLayer;

    
    protected Rigidbody2D    rb;
    protected SpriteRenderer spriteRenderer;
    protected float          invulnerabilityTimer = 0;
    protected float          blinkerTimer = 0;
    protected int vida;
    public int nHearts => vida;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        vida = vidaMAx;
    }

    protected virtual void Update()
    {

        if(invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;

            blinkerTimer -= Time.deltaTime;
            if(blinkerTimer <= 0)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                blinkerTimer = blinkerDuration;
            }
        }
        else
        {
            spriteRenderer.enabled = true;
            
        }
        
    }

    protected bool isGround() 
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheckObject.position, groundCheckRadius, groundCheckLayer);
        return (collider != null);
    }

    protected bool isGroundFront() 
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheckFront.position, groundCheckRadius, groundCheckLayer);
        return (collider != null);
    }

    public void takeDamage(int dano)
    {
        if(invulnerabilityTimer > 0) return;
        if(vida <=   0) return;

        vida -= dano;
        
        if(vida <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            invulnerabilityTimer = invulnerabilityDuration;
            blinkerTimer = blinkerDuration;
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheckObject.position, groundCheckRadius);

        if(groundCheckFront != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(groundCheckFront.position, groundCheckRadius);
        }
    }
    
}

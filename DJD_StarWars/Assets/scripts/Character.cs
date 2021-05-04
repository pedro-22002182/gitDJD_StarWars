using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    protected float vida = 100;

    [SerializeField]
    protected float dano = 10;

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

    
    protected Rigidbody2D rb;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    public void takeDamage(float dano)
    {
        vida -= dano;
        if(vida < 0)
        {
            vida = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.GetComponent<Character>();

        if(character != null)
        {
            character.takeDamage(dano);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimoBase : MonoBehaviour
{

    [SerializeField]
    private float vida = 100;

    [SerializeField]
    private float dano = 10;

    [SerializeField]
    private Transform groundCheckObject;

    [SerializeField]
    private Transform groundCheckFront;

    [SerializeField]
    private float groundCheckRadius;

    [SerializeField]
    private LayerMask groundCheckLayer;

    //Propriedades
    public bool IsGround {get; private set;}
    public bool IsGroundFront {get; private set;}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheckObject.position, groundCheckRadius, groundCheckLayer);
        IsGround = (collider != null);

        Collider2D colliderFront = Physics2D.OverlapCircle(groundCheckFront.position, groundCheckRadius, groundCheckLayer);
        IsGroundFront = (colliderFront != null);

    }

    public void takeDamage(float dano)
    {
        vida -= dano;
        if(vida < 0)
        {
            vida = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheckObject.position, groundCheckRadius);

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(groundCheckFront.position, groundCheckRadius);
    }
}

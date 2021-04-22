using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimoBase : MonoBehaviour
{

    [SerializeField]
    private float vida = 100;

    [SerializeField]
    private float dano = 10;

    private bool isGround;
    private bool hasGroundFront;

    [SerializeField]
    private Transform groundCheckObject;

    [SerializeField]
    private Transform groundCheckFront;

    [SerializeField]
    private float groundCheckRadius;

    [SerializeField]
    private LayerMask groundCheckLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheckObject.position, groundCheckRadius, groundCheckLayer);
        isGround = (collider != null);

        Collider2D colliderFront = Physics2D.OverlapCircle(groundCheckFront.position, groundCheckRadius, groundCheckLayer);
        hasGroundFront = (colliderFront != null);

    }

    public void takeDamage(float dano)
    {
        vida -= dano;
        if(vida < 0)
        {
            vida = 0;
        }
    }

    public float getDano()
    {
        return dano;
    }

    public bool GroundCheck()
    {
        return isGround;
    }

    public bool GroundFrontCheck()
    {
        return hasGroundFront;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheckObject.position, groundCheckRadius);

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(groundCheckFront.position, groundCheckRadius);
    }
}

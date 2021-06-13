using System.Collections;
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
    protected float       groundCheckRadius = 3.0f;
    [SerializeField]
    protected LayerMask   groundCheckLayer;

    
    protected Rigidbody2D    rb;
    protected SpriteRenderer spriteRenderer;
    protected float          invulnerabilityTimer = 0;
    protected float          blinkerTimer = 0;
    protected int vida;
    public int nHearts => vida;

    protected Animator animator;

    //Sons
    [SerializeField]
    private AudioSource hitSound;

    [SerializeField]
    private AudioClip dieSound;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

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
        
        animator.SetFloat("AbsSpeedX", Mathf.Abs(rb.velocity.x));
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
            onDeath();
        }
        else
        {
            invulnerabilityTimer = invulnerabilityDuration;
            blinkerTimer = blinkerDuration;


            hitSound.pitch = Random.Range(0.8f, 1.2f);
            hitSound.volume = Random.Range(0.7f, 1f);
            hitSound.Play();
        }
    }

    public int getVidaMax()
    {
        return vidaMAx;
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

    protected virtual void onDeath()
    {
        SoundManager.instance.PlaySound(dieSound, Random.Range(0.7f, 1f), Random.Range(0.8f, 1.2f));
        Destroy(this.gameObject);
    }
}

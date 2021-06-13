using System.Collections;
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
    private float       radiusAtack;
    private float tempForDamageAtack;


    //MANA COntrol
    private ManaUiPlayer manaBar;
    [SerializeField]
    public int MaxMana {get; set;}
    public float CurrentMana {get; set;}

    [SerializeField]
    private bool haveSabre; //se tem o sabre 
    private bool canAtack; //se tem o sabre ativo ou nao
    private bool isAtack;

    [SerializeField]
    private float tempMaxAtackMana; //se tem o sabre ou nao
    private float tempAtack;

    private float       hAxis;
    private int         nJumps;
    private float       timeOfJump;


    //RASTEIRA
    float maxSpeed;
    bool rasteira;

    
    [SerializeField]
    private float boostRasteira;

    private ForceManagent managerForce;

    //variavel que é true quando o boss faz força no player
    public bool DamageForce {get; set;}


    //pontoSpawn
    Transform safeSpwanPoint;


    //Sounds
    [SerializeField]
    private AudioSource dashSound;
    [SerializeField]
    private AudioSource jumpSound;
    [SerializeField]
    private AudioSource lightSaber;
    [SerializeField]
    private AudioSource lightSaberOff;
    [SerializeField]
    private AudioSource lightSaberAtack;
    [SerializeField]
    private AudioSource lightSaberDefend;
    [SerializeField]
    private AudioClip destruirSound;

    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        manaBar = FindObjectOfType<ManaUiPlayer>(); 
        managerForce = FindObjectOfType<ForceManagent>(); 

        MaxMana = 100;
        CurrentMana = MaxMana;
        manaBar.SetMaxMana(MaxMana);

        maxSpeed = moveSpeed;


        SafeSpawnPoint ssp = FindObjectOfType<SafeSpawnPoint>(); 
        if (ssp) safeSpwanPoint = ssp.transform;
        else
        {
            GameObject go = new GameObject();
            go.name = "SafeSpawnPoint";
            go.AddComponent<SafeSpawnPoint>();

            safeSpwanPoint = go.transform;
        }

        if(haveSabre)
        {
            canAtack = true;
        }
    }

    void FixedUpdate()
    {
        if(DamageForce == false)
        {
            rb.velocity = new Vector2(hAxis * moveSpeed, rb.velocity.y);

            if(onForeground())
            {
                safeSpwanPoint.position = transform.position;
            }
        }
        
    }

    // Update is called once per frame
    protected override void Update()
    {   
        Debug.Log(PlayerPrefs.GetInt("mortes"));
        //para a luta com o boss
        if(DamageForce == false)
        {
            base.Update();
            
            hAxis = Input.GetAxis("Horizontal");

            if((isGround()) && (Mathf.Abs(rb.velocity.y) < 0.1f))
            {
                nJumps = maxJumps;
            }

            if((Input.GetButtonDown("Jump") || Input.GetKeyDown("w"))&& nJumps > 0)  //isGround()
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                nJumps -= 1;

                rb.gravityScale = jumpGravityStart; 
                
                timeOfJump = Time.time; 

                jumpSound.pitch = Random.Range(0.6f, 0.9f);
                jumpSound.volume = Random.Range(0.45f, 0.75f);
                jumpSound.Play();
            }
            else
            {
                float elapsedTimeSinceJump = Time.time - timeOfJump;

                if((Input.GetButton("Jump") || Input.GetKeyDown("w")) && (elapsedTimeSinceJump < maxJumpTime))
                {
                    rb.gravityScale = jumpGravityStart;
                }
                else
                {
                    rb.gravityScale = 2.5f;
                }
            }

            //Sacar ou guardar Sabre
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(haveSabre)
                {
                    canAtack = !canAtack;

                    if(canAtack)
                    {
                        lightSaber.pitch = Random.Range(0.95f, 1.05f);
                        lightSaber.volume = Random.Range(0.1f, 0.25f);
                        lightSaber.Play();
                    }
                    else
                    {
                        lightSaberOff.pitch = Random.Range(0.95f, 1.05f);
                        lightSaberOff.volume = Random.Range(0.1f, 0.25f);
                        lightSaberOff.Play();
                    }
                }
            }
            

            //RASTEIRA
            if(Input.GetKeyDown("s") && isMove() && isGround())
            {
                rasteira = true;
                moveSpeed = maxSpeed + boostRasteira;

                GetComponent<CapsuleCollider2D>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                transform.GetChild(2).GetComponent<CapsuleCollider2D>().enabled = true;
                transform.GetChild(2).GetComponent<BoxCollider2D>().enabled = true;

                dashSound.pitch = Random.Range(0.4f, 0.65f);
                dashSound.volume = Random.Range(0.25f, 0.5f);
                dashSound.Play();
                
            }
            if(Input.GetKeyUp("s") || !isGround())
            {
                rasteira = false;
                moveSpeed = maxSpeed;

                GetComponent<CapsuleCollider2D>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
                transform.GetChild(2).GetComponent<CapsuleCollider2D>().enabled = false;
                transform.GetChild(2).GetComponent<BoxCollider2D>().enabled = false;
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
            if(Input.GetMouseButtonDown(0) && canAtack && !isMove())
            {
                isAtack = true;
                tempForDamageAtack = 0.13f;
                
                
                lightSaberAtack.pitch = Random.Range(0.95f, 1.1f);
                lightSaberAtack.volume = Random.Range(0.07f, 0.15f);
                lightSaberAtack.Play();
            
            }

            if(tempForDamageAtack > 0)
            {
                tempForDamageAtack -= Time.deltaTime;

                if(tempForDamageAtack <= 0)
                {
                    Collider2D[] cols = Physics2D.OverlapCircleAll(atackFrontPos.position, radiusAtack);
                    CheckCircle(cols);
                }
            }

            if(Input.GetMouseButton(0) && canAtack&& !isMove() && tempForDamageAtack <= 0)
            {
                if(CurrentMana > 0)
                {
                    Collider2D[] cols = Physics2D.OverlapCircleAll(atackFrontPos.position, radiusAtack);
                    CheckCircle(cols);
                    isAtack = true;

                    tempAtack += Time.deltaTime;

                    if(tempAtack >= tempMaxAtackMana)
                    {
                        managerForce.ChangeMana(-1.1f);
                    }

                    if(!(lightSaberDefend.isPlaying))
                    {
                        lightSaberDefend.pitch = Random.Range(0.8f, 1.1f);
                        lightSaberDefend.volume = Random.Range(0.7f, 0.9f);
                        lightSaberDefend.Play();
                    }
                }
                else
                {
                    tempAtack = 0;
                    isAtack = false;
                    
                    if(lightSaberDefend.isPlaying)
                    {
                        lightSaberDefend.Stop();
                    }
                }
            }

            if(Input.GetMouseButtonUp(0) && canAtack && !isMove())
            {
                tempAtack = 0;
                isAtack = false;

                if(lightSaberDefend.isPlaying)
                {
                    lightSaberDefend.Stop();
                }
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

            //animator
            //animator.SetBool("jump", !isGround());
            animator.SetBool("dash", rasteira);
            animator.SetBool("canAtack", canAtack);
            animator.SetBool("atack", isAtack);
        }
        else
        {
            animator.SetFloat("AbsSpeedX", 0);
            rb.gravityScale = 0f;
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

            if (col.gameObject.CompareTag("door"))
            {   
                SoundManager.instance.PlaySound(destruirSound, 1, Random.Range(0.95f, 1.05f));
                Destroy(col.gameObject);
            }

            if(col.gameObject.CompareTag("boss"))
            {
                col.gameObject.GetComponent<inimigoBoss>().checkDanoEspada(dano);
            }
        }
    }

    public void GetSabre()
    {
        haveSabre = true;
        canAtack = true;

        lightSaber.volume = 0.15f;
        lightSaber.Play();
    }


    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        if(isAtack)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(atackFrontPos.position, radiusAtack);
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


    protected bool onForeground() 
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheckObject.position, groundCheckRadius, groundCheckLayer);

        if(collider != null)
        {
            return (collider.name == "ForeGround");
        }

        return false;
    }

    public void GotoSafe()
    {
        if(vida > 0)
        {
            transform.position = safeSpwanPoint.position;
        }   
    }

    protected override void onDeath()
    {
        PlayerPrefs.SetInt("mortes", PlayerPrefs.GetInt("mortesAntigas"));


        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("menu");        
        base.onDeath();
    }
    
}

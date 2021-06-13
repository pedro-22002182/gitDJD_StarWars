using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigoBoss : Character
{
    private GameObject target;
    private Vector3 targetPosInicial;

    private bool ativarBoss;
    [SerializeField]
    private float distAtivar;
    private float distPlayer;
    [SerializeField]
    private Dialogue dialogue;

    private ModoBoss modoBoss;
    private enum ModoBoss
    {
        repouso,
        ataqueEspada,

        ataqueForca
    };


    //repouso
    [SerializeField]
    private float maxTempRepouso;

    private float tempRepouso;

    //ataqueEspada

    private Transform posicaoInicial;

    private int directionMove;

    private bool tocouParede;


    //ataque forca

    [SerializeField]
    private float maxTempForca;
    
    private float tempForca;

    [SerializeField]
    private Transform spawnCaixas;

    [SerializeField]
    private GameObject caixa;
    private int numSpawnsCaixas;

    [SerializeField]
    private GameObject vidaBossInterface;

    //Sounds
    [SerializeField]
    private AudioSource forceSound;

    [SerializeField]
    private AudioSource ataqueSound;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        modoBoss = ModoBoss.repouso;
        tempRepouso = maxTempRepouso/3;

        directionMove = -1; //começa por ir para esquerda
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        target = GameObject.Find("player");


        distPlayer = Vector3.Distance(target.transform.position, transform.position);

        if(distAtivar >= distPlayer)
            dialogue.ativarDialogo();
            

        if(dialogue.checkAcabou())
        {
            vidaBossInterface.SetActive(true);

            if(modoBoss == ModoBoss.repouso)
            {
                if(tempRepouso > 0)
                {
                    tempRepouso -= Time.deltaTime;
                }
                else
                {
                    getModoAleatorio(Random.Range(0,5));
                }
            }

            else if(modoBoss == ModoBoss.ataqueEspada)
            {
                ataqueEspada();

                if(!(ataqueSound.isPlaying))
                {
                    ataqueSound.pitch = Random.Range(0.95f, 1.1f);
                    ataqueSound.volume = Random.Range(0.1f, 0.15f);
                    ataqueSound.Play();
                }

                if(transform.position.x <= posicaoInicial.position.x && tocouParede == true && directionMove == -1)
                {
                    ataqueSound.Stop();
                    getModoAleatorio(Random.Range(0,5));
                }
            }

            else if(modoBoss == ModoBoss.ataqueForca)
            {
                //aproxima dele, mete gajo para cima e leva dano quando player atira com caixa
                if(distPlayer < 200)
                {
                    if(tempForca > 0)
                    {
                        tempForca -= Time.deltaTime;

                        target.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

                        Vector3 posFinalTarget = new Vector3(targetPosInicial.x, targetPosInicial.y + 50, targetPosInicial.z);
                        target.transform.position = Vector3.Lerp(targetPosInicial, posFinalTarget, moveSpeed);

                        if(numSpawnsCaixas > 0)
                        {
                            SpawnCaixa();
                        }

                        if(!(forceSound.isPlaying))
                        {
                            forceSound.pitch = Random.Range(0.8f, 1.1f);
                            forceSound.volume = 1;
                            forceSound.Play();
                        }
                    }
                    else
                    {
                        target.GetComponent<Player>().DamageForce = false;
                        target.GetComponent<Player>().takeDamage(dano);

                        forceSound.Stop();
                        getModoAleatorio(Random.Range(0,5));
                        animator.SetBool("forca", false);
                    }

                    getRotation();
                }
                else
                {
                    if(transform.position.x > target.transform.position.x)
                        directionMove = -1;
                    else
                        directionMove = 1;

                    rb.velocity = new Vector2(directionMove * moveSpeed , rb.velocity.y);
                }
                
            }

        }
    }


    private void ataqueEspada()
    {
        rb.velocity = new Vector2(directionMove * moveSpeed , rb.velocity.y);
    }


    private void getRotation()
    {
        //FLIP Player
        if(transform.position.x > target.transform.position.x)
        {
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.y = 180;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
        else
        {
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.y = 0;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }

    private void getModoAleatorio(int index)
    {
        //menor que 3 (maior= 0) ent varia entre repouso e ataque espada
        if(index < 3)
        {
            if(modoBoss == ModoBoss.repouso)
            {
                tocouParede = false;
                directionMove = -1;
                posicaoInicial = transform;
                modoBoss = ModoBoss.ataqueEspada;
            }
            else
            {
                tempRepouso = (maxTempRepouso/15) * nHearts;
                modoBoss = ModoBoss.repouso;
            }
        }
        //entre 3 e 4 ataque forca ou caso ja esteja, repouso
        else
        {
            if(modoBoss == ModoBoss.repouso)
            {
                targetPosInicial = target.transform.position;
                tempForca = maxTempForca;
                target.GetComponent<Player>().DamageForce = true;

                numSpawnsCaixas = 1; //Random.Range(1,3);
                modoBoss = ModoBoss.ataqueForca;

                animator.SetBool("forca", true);
            }
            else
            {
                tempRepouso = (maxTempRepouso/15) * nHearts;
                modoBoss = ModoBoss.repouso;
            }
        }

        Debug.Log(modoBoss);
    }


    //so leva dano de espada quando esta modo repouso
    public void checkDanoEspada(int dano)
    {
        if(modoBoss == ModoBoss.repouso)
            takeDamage(dano);
    }

    private void SpawnCaixa()
    {

        Vector3 pos = new Vector3(target.transform.position.x + Random.Range(-35,5), spawnCaixas.position.y,spawnCaixas.position.z);

        GameObject gb = Instantiate(caixa, pos, Quaternion.identity);
        gb.transform.localScale = new Vector3(Random.Range(0.9f,1.2f),Random.Range(0.9f,1.2f),Random.Range(0.9f,1.2f));
        
        numSpawnsCaixas -= 1;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if(collision.gameObject.name == "player" && modoBoss != ModoBoss.repouso)
        {   
            Character character = collision.GetComponent<Character>();
            character.takeDamage(dano);
        }

        if(collision.gameObject.tag == "caixa" && modoBoss == ModoBoss.ataqueForca)
        {
            takeDamage(1);
            target.GetComponent<Player>().DamageForce = false;
            forceSound.Stop();

            getModoAleatorio(Random.Range(0,5));
            Destroy(collision.gameObject);

            animator.SetBool("forca", false);
        }
        else if(collision.gameObject.tag == "caixa")
        {
            Destroy(collision.gameObject);
        }
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distAtivar);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "parede" || col.gameObject.name == "player")
        {
            directionMove *= -1;
            tocouParede = true;

            //FLIP Player
            if(directionMove == -1)
            {
                Vector3 currentRotation = transform.rotation.eulerAngles;
                currentRotation.y = 180;
                transform.rotation = Quaternion.Euler(currentRotation);
            }
            else if(directionMove == 1)
            {
                Vector3 currentRotation = transform.rotation.eulerAngles;
                currentRotation.y = 0;
                transform.rotation = Quaternion.Euler(currentRotation);
            }
            
        }
    }

    protected override void onDeath()
    {
        Destroy(this.gameObject);
        GameObject.Find("Fim").GetComponent<Final>().comecarFim = true;
        base.onDeath();
    }

    
}

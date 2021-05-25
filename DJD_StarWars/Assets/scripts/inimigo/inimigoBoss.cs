using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigoBoss : Character
{
    private GameObject target;
    private Transform targetPosInicial;

    private bool ativarBoss;
    [SerializeField]
    private float distAtivar;
    private float distPlayer;



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


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        modoBoss = ModoBoss.repouso;
        tempRepouso = maxTempRepouso;

        directionMove = -1; //começa por ir para esquerda
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        target = GameObject.Find("player");


        distPlayer = Vector3.Distance(target.transform.position, transform.position);

        if(distAtivar <= distPlayer)
            ativarBoss = true;



        if(ativarBoss)
        {
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

                if(transform.position.x <= posicaoInicial.position.x && tocouParede == true && directionMove == -1)
                {
                    getModoAleatorio(Random.Range(0,5));
                }
            }

            else if(modoBoss == ModoBoss.ataqueForca)
            {
                //mete gajo para cima e leva dano quando player atira com caixa

                if(tempForca > 0)
                {
                    tempForca -= Time.deltaTime;

                    Vector3 posFinalTarget = new Vector3(targetPosInicial.position.x, targetPosInicial.position.y + 5, targetPosInicial.position.z);
                    target.transform.position = Vector3.Lerp(targetPosInicial.position, posFinalTarget, moveSpeed);
                    
                }
                else
                {
                    target.GetComponent<Player>().DamageForce = false;
                    getModoAleatorio(Random.Range(0,5));
                }
            }

        }
    }


    private void ataqueEspada()
    {
        rb.velocity = new Vector2(directionMove * moveSpeed , rb.velocity.y);
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
                tempRepouso = maxTempRepouso;
                modoBoss = ModoBoss.repouso;
            }
        }
        //entre 3 e 4 ataque forca ou caso ja esteja, repouso
        else
        {
            if(modoBoss == ModoBoss.repouso)
            {
                targetPosInicial = target.transform;
                tempForca = maxTempForca;
                target.GetComponent<Player>().DamageForce = true;
                modoBoss = ModoBoss.ataqueForca;
            }
            else
            {
                tempRepouso = maxTempRepouso;
                modoBoss = ModoBoss.repouso;
            }
        }
    }


    //so leva dano de espada quando esta modo repouso
    public void checkDanoEspada(int dano)
    {
        if(modoBoss == ModoBoss.repouso)
        {
            takeDamage(dano);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if(collision.gameObject.name == "player" && modoBoss != ModoBoss.repouso)
        {   
            Character character = collision.GetComponent<Character>();
            character.takeDamage(dano);
        }

        if(collision.gameObject.name == "caixa" && modoBoss == ModoBoss.ataqueForca)
        {
            takeDamage(1);
            target.GetComponent<Player>().DamageForce = false;
            getModoAleatorio(Random.Range(0,5));
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "parede")
        {
            directionMove *= -1;
            tocouParede = true;
        }

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigoMovement : MonoBehaviour
{

    [SerializeField]
    private float speed;
    private int direction = -1; //left movement
    private Rigidbody2D rb;

    
    //Dists Movement
    [SerializeField]
    private float distMaxMov;
    private float distMinMov;


    //target = player
    private Transform target;
    private float distPlayer;

    


    // Start is called before the first frame update
    void Start()
    {   
        //so, when enemy attacks is stopped
        float distAtack = GetComponent<inimigoAtaque>().getDistAttack();
        distMinMov = distAtack;

        rb = this.GetComponent<Rigidbody2D>();


        //E METER QUE SO ANDA SE TIVER NO CHAO
        // METER O GAJO A não cair, ou seja meter aquilo no chao á frente player e so movimentar se houver chao
    }


    // Update is called once per frame
    void Update()
    {
        
        if(GetComponent<inimoBase>().GroundCheck() == true)
        {
            target = GameObject.Find("player").transform;


            //dist between player and enemy
            distPlayer = Vector3.Distance(target.position, transform.position);
            setDirection();

            //enemy follow player between distMax and distMin
            if(distPlayer < distMaxMov && distPlayer > distMinMov)
            {
                followPlayer();
            }
        }
    }

    private void followPlayer()
    {
        rb.velocity = new Vector2(direction * speed , rb.velocity.y);
    }



    private void setDirection()
    {
        
        if(target.position.x < transform.position.x)
        {
            direction = -1;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        //if player is on right (player.x > this.x), direcion is 1 and enemy rotation is 180, 
        else if(target.position.x > transform.position.x)
        {
            direction = 1;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
}

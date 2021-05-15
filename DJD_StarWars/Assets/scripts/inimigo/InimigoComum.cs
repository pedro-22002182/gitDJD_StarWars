using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoComum : Character
{

    //target = player
    private Transform target;
    //Dists Movement
    [SerializeField]
    private float distMaxMov;
    
    
    [SerializeField]
    private float cooldownTimeAttack = 1.2f; 
    float timerAttack;
    [SerializeField]
    private float distAtack;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform arm;


    private int directionMove = -1; //left movement
    private float distPlayer;
    private float distMinMov;

    //levarDanoQueda por causa forca
    private bool danoQueda;
    

    // Start is called before the first frame update
    protected override void Start()
    {   
        base.Start();

        //so, when enemy attacks is stopped
        distMinMov = distAtack;
        
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        target = GameObject.Find("player").transform;
        setDirection();

        //dist between player and enemy
        distPlayer = Vector3.Distance(target.position, transform.position);

        if(isGroundFront())
        {
            //enemy follow player between distMax and distMin
            if(distPlayer < distMaxMov && distPlayer > distMinMov)
            {
                followPlayer();
            }
        }

        if(isGround() == true)
        {

            //if dist is short than distAtack 
            if(distPlayer <= distAtack)
            {
                timerAttack += Time.deltaTime;
                rotationArm();

                if(timerAttack >= cooldownTimeAttack)
                {
                    Fire(target);
                    timerAttack = 0;
                }
            }
        }

        
    }

    private void followPlayer()
    {
        rb.velocity = new Vector2(directionMove * moveSpeed , rb.velocity.y);
    }


    private void setDirection()
    {
        
        if(target.position.x < transform.position.x)
        {
            directionMove = -1;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        //if player is on right (player.x > this.x), direcion is 1 and enemy rotation is 180, 
        else if(target.position.x > transform.position.x)
        {
            directionMove = 1;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }


    //spawn bullet at arm position
    void Fire(Transform target)
    {
        //arm rotation + random rotation in z to do a bullet "imprevissivel"
        Vector3 posRot = arm.rotation.eulerAngles;
        posRot.z += Random.Range(-15,10);
        Quaternion quaternion = Quaternion.Euler(posRot.x, posRot.y, posRot.z);

        GameObject firedBullet = Instantiate(bullet, arm.position, quaternion);

        animator.SetTrigger("shot");
    }

    //update rotation arm
    void rotationArm()
    {
        Vector2 lookDirection = target.position - this.transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        arm.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);
    }

    //por causa Force
    public void levarDanoQueda()
    {
        danoQueda = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if(collision.gameObject.name == "player")
        {   
            Character character = collision.GetComponent<Character>();
            character.takeDamage(dano);
            Debug.Log("dano");
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("ground") && danoQueda == true)
        {
            takeDamage(1);
            Debug.Log("danoQueda");
            danoQueda = false;
        }
    }


    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();



        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distMaxMov);


        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distAtack);

    }
}

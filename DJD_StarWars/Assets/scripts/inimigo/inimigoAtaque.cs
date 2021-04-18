using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigoAtaque : MonoBehaviour
{

    [SerializeField]
    private float cooldownTime = 0.5f; 
    float timer;

    [SerializeField]
    private float distAtack = 2f;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject arm;

    //target jogador
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        //FAZER
        //meter coldTime random
        //bullet ter o seu proprio script com mov e destori e tal
        //perceber cena do braco
        //arranjar variaveis
        //descrever melhor cenas
    }

    void FixedUpdate()
    {
        target = GameObject.Find("player").transform;

        rotationArm();
    }

    // Update is called once per frame
    void Update()
    {
        //distancia entre jogador e inimigo
        float distPlayer = Vector3.Distance(target.position, this.gameObject.transform.position);

        //se a dist for menor do que a dist Ataque
        if(distPlayer <= distAtack)
        {
            timer += Time.deltaTime;

            //inimigo dispara de cooldownTime em cooldownTime tempo
            if(timer >= cooldownTime)
            {
                Fire(target);
                timer = 0;
            }
        }
    }

    //spawn Tiro
    void Fire(Transform target)
    {
        GameObject firedBullet = Instantiate(bullet, arm.transform.position, arm.transform.rotation);
        firedBullet.GetComponent<Rigidbody2D>().velocity = arm.transform.up * 12f;
    }

    //braco atualizar pos consoante player
    void rotationArm()
    {
        Vector2 lookDirection = target.position - this.transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        arm.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);
    }
}

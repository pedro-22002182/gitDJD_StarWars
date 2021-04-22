using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigoAtaque : MonoBehaviour
{

    [SerializeField]
    private float cooldownTime = 1.2f; 
    float timer;

    [SerializeField]
    private float distAtack;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform arm;

    //target = player
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        //FAZER
        //meter pos direcao tiro + random em y para dar aquela cena de não ser sempre em cima player
    }

    void FixedUpdate()
    {
        target = GameObject.Find("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //dist between player and enemy
        float distPlayer = Vector3.Distance(target.position, this.gameObject.transform.position);

        //if dist is short than distAtack 
        if(distPlayer <= distAtack)
        {
            timer += Time.deltaTime;
            rotationArm();

            if(timer >= cooldownTime)
            {
                Fire(target);
                timer = 0;
            }
        }
    }

    //spawn bullet at arm position
    void Fire(Transform target)
    {
        GameObject firedBullet = Instantiate(bullet, arm.position, arm.rotation);
    }

    //update rotation arm
    void rotationArm()
    {
        Vector2 lookDirection = target.position - this.transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        arm.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);
    }

    public float getDistAttack()
    {
        return distAtack;
    }
}

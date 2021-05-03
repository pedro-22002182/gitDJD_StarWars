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

    }

    void FixedUpdate()
    {
        target = GameObject.Find("player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(GetComponent<inimoBase>().IsGround == true)
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
    }

    //spawn bullet at arm position
    void Fire(Transform target)
    {
        //arm rotation + random rotation in z to do a bullet "imprevissivel"
        Vector3 posRot = arm.rotation.eulerAngles;
        posRot.z += Random.Range(-15,5);
        Quaternion quaternion = Quaternion.Euler(posRot.x, posRot.y, posRot.z);

        GameObject firedBullet = Instantiate(bullet, arm.position, quaternion);
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

    void onTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            //playerBase = col.gameObject.GetComponent
            //vidaPlayer - dano;

            //e ter uma assim para o tiro?
        }
    }
}

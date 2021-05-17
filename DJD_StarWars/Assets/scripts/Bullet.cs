using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveVector;

    [SerializeField]
    private float speed = 400;

    [SerializeField]
    private int dano = 1;

    float   timer = 0.0f;
    Vector3 startPos;

    private int nRicochete = 0;

    void Start()
    {
        //use object diretion   
        moveVector = transform.up;
        moveVector.Normalize();

        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        transform.position = startPos + moveVector * speed * timer;
    }

    void Update()
    {
        
    }

    public void Ricochete()
    {
        if(nRicochete == 0)
        {
            startPos = transform.position;
            timer = 0;
            speed *= Random.Range(-1.2f,-1.6f);

            moveVector = new Vector3(Random.Range(-1f,1f),0,0) + transform.up;
            moveVector = moveVector.normalized;

            nRicochete += 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if(collision.gameObject.name == "player")
        {   
            Character character = collision.GetComponent<Character>();
            character.takeDamage(dano);
        }
        
        if(collision.gameObject.tag == "enemy" && nRicochete > 0)
        {
            Character character = collision.GetComponent<Character>();
            character.takeDamage(dano);
        }
    }
}
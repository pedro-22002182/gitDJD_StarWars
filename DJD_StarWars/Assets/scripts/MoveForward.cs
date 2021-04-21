using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Vector3 moveVector;

    [SerializeField]
    private float speed = 400;

    float   timer = 0.0f;
    Vector3 startPos;

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
}
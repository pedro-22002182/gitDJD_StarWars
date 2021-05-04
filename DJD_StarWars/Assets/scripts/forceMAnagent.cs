using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceMAnagent : MonoBehaviour
{
    //Variables for held
    private Vector3 startPos;
    private bool isBeingHeld = false;


    //Variables for throw
    private float timeForThrow = 0.025f;

    [SerializeField]
    private float speedThorw = 110;

    private bool isThrow;
    private float timeThrow;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        isThrow = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingHeld == true)
        {
            Vector3 mousePos = GetMousePos();
            Vector3 endPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);

            transform.position = Vector3.Lerp(startPos, endPos, 0.92f);
        }


        if(isThrow == false && isBeingHeld == false)
        { 
            timeThrow += Time.deltaTime;

            if(timeThrow > timeForThrow)
            {
                Vector3 mousePos = GetMousePos();
                Vector3 endPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);
                Vector3 direction = (transform.position - endPos).normalized;

                float distSpeed = Vector3.Distance(endPos, transform.position);
                
                rb.AddForce(direction * -distSpeed * speedThorw);

                isThrow = true;
                timeThrow = 0;
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            startPos = transform.position;
            isBeingHeld = true;
            rb.gravityScale = 0;   
        }

        if(Input.GetMouseButtonUp(1))
        {
            isBeingHeld = false;
            rb.gravityScale = 1; 
        }
    }

    private void OnMouseExit()
    {   
        if(isBeingHeld == true)
        {
            isBeingHeld = false;
            isThrow = false;
        }
        rb.gravityScale = 1; 
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return mousePos;
    }


    /* private void OnMouseDown()
    {
        startPos = transform.position;
        isBeingHeld = true;
        rb.gravityScale = 0;   
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
        rb.gravityScale = 1;  
    } */
}

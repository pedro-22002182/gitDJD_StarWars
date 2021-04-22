using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceMAnagent : MonoBehaviour
{

    private Vector3 startPos;
    private bool isBeingHeld = false;

    //Throw
    [SerializeField]
    private float timeThrow;

    [SerializeField]
    private float speedThorw;

    private bool Throw;
    private float time;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        Throw = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingHeld == true)
        {
            
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 endPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);

            transform.position = Vector3.Lerp(startPos, endPos, 0.85f);

            rb = transform.GetComponent<Rigidbody2D>();
        }


        if(Throw == false && isBeingHeld == false)
        { 
            time += Time.deltaTime;

            if(time > timeThrow)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                Vector3 endPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);

                Vector3 direction = (transform.position - endPos).normalized;
                float distSpeed = Vector3.Distance(endPos, transform.position);
                
                rb.AddForce(direction * -distSpeed * speedThorw);
                Throw = true;
                time = 0;
            }
        }
    }

    private void OnMouseDown()
    {
        startPos = transform.position;
        isBeingHeld = true;
        rb.gravityScale = 0;   

    }

    private void OnMouseUp()
    {
        rb.gravityScale = 1;  
        isBeingHeld = false;
    }

    private void OnMouseExit()
    {   
        rb.gravityScale = 1; 
        if(isBeingHeld == true)
        {
            isBeingHeld = false;
            Throw = false;
        }
    }
}

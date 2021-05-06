using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceApply : MonoBehaviour
{
    //Variables for held
    private Vector3 startPos;
    private bool isBeingHeld = false;
    private bool canHeld;


    private bool isThrow;
    private float timeThrow;
    private Rigidbody2D rb;


    private float tempRecover;

    private forceMAnagent fManager;

    // Start is called before the first frame update
    void Start()
    {
        fManager = FindObjectOfType<forceMAnagent>();

        isThrow = true;
        canHeld = false;
        rb = GetComponent<Rigidbody2D>();

        tempRecover = fManager.getTempRecover();
    }

    // Update is called once per frame
    void Update()
    {
        int currentMana = fManager.getCurrentMana();

        if(isBeingHeld == true && currentMana > 0)
        {
            Vector3 mousePos = fManager.GetMousePos();
            Vector3 endPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);

            transform.position = Vector3.Lerp(startPos, endPos, 0.92f);
            
            fManager.ChangeMana(-1);
        }

        if(currentMana <= 0)
        {
            isBeingHeld = false;
            isThrow = true;
            rb.gravityScale = 1; 
            canHeld = false;
        }


        if(isThrow == false && isBeingHeld == false && currentMana > 0)
        { 
            timeThrow += Time.deltaTime;

            if(timeThrow > fManager.getTimeForThrow())
            {
                Vector3 mousePos = fManager.GetMousePos();
                Vector3 endPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);
                Vector3 direction = (transform.position - endPos).normalized;

                float distSpeed = Vector3.Distance(endPos, transform.position);
                
                rb.AddForce(direction * -distSpeed * fManager.getSpeedThorw());

                isThrow = true;
                timeThrow = 0;
            }
        }

        if(canHeld)
        {
            if(Input.GetMouseButtonDown(1))
            {
                startPos = transform.position;
                isBeingHeld = true;
                rb.gravityScale = 0;   

                tempRecover = fManager.getTempRecover();;
            }

            if(Input.GetMouseButtonUp(1))
            {
                isBeingHeld = false;
                rb.gravityScale = 1; 
            }
        }
        

        if(!(Input.GetMouseButton(1)))
        {
            if(tempRecover <= 0)
            {
                fManager.ChangeMana(+1);
            }
            else
            {
                tempRecover -= Time.deltaTime;
            }
        }
    }

    private void OnMouseEnter()
    {
        canHeld = true;
    }
    private void OnMouseExit()
    {   
        if(isBeingHeld == true)
        {
            isBeingHeld = false;
            isThrow = false;
        }
        rb.gravityScale = 1; 

        canHeld = false;
    }
}

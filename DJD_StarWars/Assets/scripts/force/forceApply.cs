using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forceApply : MonoBehaviour
{
    [SerializeField]
    private float speedHoldForce = 0.92f;  

    [SerializeField]
    private float forcaGasta;  


    //Variables for held
    private Vector3 startPos;
    private bool isBeingHeld = false;
    private bool canHeld;


    private bool isThrow;
    private float timeThrow;
    private Rigidbody2D rb;



    private forceMAnagent fManager;

    [SerializeField]
    private bool moveOnlyY;

    [SerializeField]
    private float maxY;

    [SerializeField]
    private float minY;


    [SerializeField]
    private bool moveOnlyX;

    [SerializeField]
    private float maxX;

    [SerializeField]
    private float minX;

    private bool lockObject;


    

    // Start is called before the first frame update
    void Start()
    {
        fManager = FindObjectOfType<forceMAnagent>();

        isThrow = true;
        canHeld = false;
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float currentMana = fManager.getCurrentMana();

        if(isBeingHeld == true && currentMana > 0 && !lockObject && fManager.isMove() == false)
        {
            Vector3 mousePos = fManager.GetMousePos();
            Vector3 endPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);
            
            if(moveOnlyY)
            {
                if(transform.position.y > maxY)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    lockObject = true;
                }
                else if(endPos.y < minY)
                {
                    endPos = new Vector3(transform.position.x, minY+0.1f, transform.position.z);
                }
                else
                {
 
                    endPos = new Vector3(transform.position.x, endPos.y, transform.position.z);
                }
            }

            if(moveOnlyX)
            {
                if(endPos.x > maxX)
                {
                    endPos = new Vector3(maxX-0.1f, transform.position.y, transform.position.z);
                }
                else if(transform.position.x < minX)
                {
                    endPos = new Vector3(minX+0.1f, transform.position.y, transform.position.z);
                }
                else
                {
                    endPos = new Vector3(endPos.x, transform.position.y, transform.position.z);
                }

            }
            

            transform.position = Vector3.Lerp(startPos, endPos, speedHoldForce);
            fManager.ChangeMana(-forcaGasta);
            
        }
        else
        {
            if(moveOnlyX)
            {
                rb.velocity = new Vector3(0,rb.velocity.y,0);

                if(transform.position.x > maxX)
                {
                    transform.position = new Vector3(maxX-0.1f, transform.position.y, transform.position.z);
                }
                else if(transform.position.x < minX)
                {
                    transform.position = new Vector3(minX+0.1f, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                }
            }
        }


        if(currentMana <= 0 || fManager.isMove() == true)
        {
            isBeingHeld = false;
            isThrow = true;
            rb.gravityScale = 1; 
            fManager.getImageEffect().color = new Color32(115,227,241,0);
        }

        //ATIRAR OBJECTO OU INIMIGO
        if(isThrow == false && isBeingHeld == false && currentMana > 0 && moveOnlyY == false && moveOnlyX == false)
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

                //Debug.Log(distSpeed);
                //levar dano queda
                if((GetComponent<InimigoComum>() != null) && distSpeed > 30)
                {
                    GetComponent<InimigoComum>().levarDanoQueda();
                }
            }
        }

        if(canHeld)
        {
            if(Input.GetMouseButtonDown(1))
            {
                startPos = transform.position;
                isBeingHeld = true;
                rb.gravityScale = 0;   

                fManager.getImageEffect().color = new Color32(115,227,241,51);
            }

            if(Input.GetMouseButtonUp(1))
            {
                isBeingHeld = false;
                rb.gravityScale = 1; 

                fManager.getImageEffect().color = new Color32(115,227,241,0);
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
            fManager.getImageEffect().color = new Color32(115,227,241,0);

            isBeingHeld = false;
            isThrow = false;
        }
        rb.gravityScale = 1; 

        canHeld = false;
    }
}

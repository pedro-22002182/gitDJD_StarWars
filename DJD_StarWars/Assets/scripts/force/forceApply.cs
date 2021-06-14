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


    //Variables for held
    private Vector3 startPos;
    private bool isBeingHeld = false;
    private bool canHeld;


    private bool isThrow;
    private float timeThrow;
    private Rigidbody2D rb;

    private bool lockObject;
    private ForceManagent fManager;


    //sons
    [SerializeField]
    private AudioSource colSound;
    

    // Start is called before the first frame update
    void Start()
    {
        fManager = FindObjectOfType<ForceManagent>();
        rb = GetComponent<Rigidbody2D>();

        isThrow = true;
        canHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        float currentMana = fManager.getCurrentMana();

        //Se poder agarrar o objeto (tendo rato em cima)
        if(canHeld)
        {
            if(Input.GetMouseButtonDown(1))
            {
                startPos = transform.position;
                rb.gravityScale = 0;   

                isBeingHeld = true;
                fManager.getImageEffect().color = new Color32(0,161,128,255);

                if(currentMana <= 0 || lockObject == true)
                    fManager.PlayLockSound();
            }

            if(Input.GetMouseButtonUp(1))
            {
                isBeingHeld = false;
                rb.gravityScale = 1; 

                fManager.getImageEffect().color = new Color32(0,161,128,0);
            }
        }


        //Se o estiver a segurar e com mana > 0 e nao estiver trancado e o player parado
        if(isBeingHeld == true && currentMana > 0 && !lockObject && fManager.isMove() == false)
        {
            rb.velocity = new Vector3(0,0,0); 
            Vector3 mousePos = fManager.GetMousePos();
            Vector3 endPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);
            
            //movimento apenas em Y
            if(moveOnlyY)
            {
                if(transform.position.y > maxY)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    lockObject = true;
                }
                else if(endPos.y < minY)
                    endPos = new Vector3(transform.position.x, minY, transform.position.z);
                else
                    endPos = new Vector3(transform.position.x, endPos.y, transform.position.z);

                transform.position = Vector3.Lerp(startPos, endPos, speedHoldForce);
            }
            //movimento apenas em X
            else if(moveOnlyX)
            {
                if(endPos.x > maxX)
                    endPos = new Vector3(maxX, transform.position.y, transform.position.z);

                else if(transform.position.x < minX)
                    endPos = new Vector3(minX, transform.position.y, transform.position.z);

                else
                {
                    endPos = new Vector3(endPos.x, transform.position.y, transform.position.z);
                    transform.position = Vector3.Lerp(startPos, endPos, speedHoldForce);
                }
            }
            //Movimento Livre
            else
            {
                transform.position = Vector3.Lerp(startPos, endPos, speedHoldForce);
            }

            fManager.ChangeMana(-forcaGasta);
            fManager.PlayForceSound();
        }
        else
        {
            lockMoveX();
        }

        //sem mana ou o player a mexer a força não é possivel, nem atirar objeto
        if(currentMana <= 0 || fManager.isMove() == true)
        {
            isBeingHeld = false;
            isThrow = true;

            rb.gravityScale = 1; 

            fManager.getImageEffect().color = new Color32(0,161,128,0);
            fManager.StopForceSound();
        }

        //ATIRAR OBJECTO/INIMIGO
        if(isThrow == false && isBeingHeld == false && currentMana > 0 && (!(moveOnlyY) && (!(moveOnlyX))))
        { 
            timeThrow += Time.deltaTime;
            
            if(timeThrow > fManager.getTimeForThrow())
            {
                Vector3 mousePos = fManager.GetMousePos();
                Vector3 endPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);
                Vector3 direction = (transform.position - endPos).normalized;

                float distSpeed = Vector3.Distance(endPos, transform.position);
                
                rb.AddForce(direction * -distSpeed * fManager.getSpeedThorw());

                //inimigo levar dano queda
                if((GetComponent<InimigoComum>() != null) && distSpeed > 30)
                    GetComponent<InimigoComum>().levarDanoQueda();
                
                isThrow = true;
                timeThrow = 0;
                fManager.PlayAtirarSound();
            }
        }        
    }

    private void OnMouseEnter() => canHeld = true;

    private void OnMouseExit()
    {   
        if(isBeingHeld == true)
        {
            isBeingHeld = false;
            isThrow = false;

            fManager.getImageEffect().color = new Color32(0,161,128,0);
            fManager.StopForceSound();
        }

        rb.gravityScale = 1; 
        canHeld = false;
    }


    //quando o objeto não está a ser mexido
    private void lockMoveX()
    {
        if(moveOnlyX)
        {
            rb.velocity = new Vector3(0,rb.velocity.y,0);

            if(transform.position.x > maxX)
                transform.position = new Vector3(maxX-0.1f, transform.position.y, transform.position.z);

            else if(transform.position.x < minX)
                transform.position = new Vector3(minX+0.1f, transform.position.y, transform.position.z);
            
            else
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        float dist = Vector3.Distance(transform.position, GameObject.Find("player").transform.position);

        if(colSound && dist < 350 && col.gameObject.layer == LayerMask.NameToLayer("ground") && !(colSound.isPlaying))
        {
            colSound.pitch = Random.Range(0.5f, 1.3f);
            colSound.volume = Random.Range(0.1f, 0.6f);
            colSound.Play();
        }
    }
}

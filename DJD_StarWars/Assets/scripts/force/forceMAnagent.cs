using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceManagent : MonoBehaviour
{
    
    //Variables for throw
    [SerializeField]
    private float timeForThrow = 0.025f;

    [SerializeField]
    private float speedThorw = 110;

    //effect
    [SerializeField]
    private Image image;


    [SerializeField]
    private float tempForRecover = 0;
    private float tempRecover;
    float tempForce = 0;


    Player player;
    ManaUiPlayer manaBar;
    
    
    //sounds
    [SerializeField]
    private AudioSource forceSound;
    [SerializeField]
    private AudioSource lockSound;
    [SerializeField]
    private AudioSource atirarSound;

    //animator
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
            player = FindObjectOfType<Player>(); 

        manaBar = FindObjectOfType<ManaUiPlayer>(); 
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        //recuperar Mana
        if(!(Input.GetMouseButton(1)) && !(Input.GetMouseButton(0)))
        {
            animator.SetBool("forca", false);

            if(tempRecover <= 0 && getCurrentMana() != 100)
            {
                ChangeMana(+1);
            }
            else
            {
                tempRecover -= Time.deltaTime;
            }
        }
        else
        {
            tempRecover = tempForRecover;
        }

        //Stop Sound Force
        if(Input.GetMouseButton(1) == false || getCurrentMana() <= 0)
        {
            forceSound.Stop();
        }
    }



    public float getSpeedThorw() => speedThorw;

    public float getTimeForThrow() => timeForThrow;

    public float getCurrentMana() => player.CurrentMana;

    public Image getImageEffect() => image;
    
    
    public void ChangeMana(float value)
    {
        tempForce += Time.deltaTime;

        if(tempForce > 0.02f)
        {   
            if(player.CurrentMana <= player.MaxMana && player.CurrentMana >= 0)
            {
                player.CurrentMana += value;
            }
            else if (player.CurrentMana < 0)
            {
                player.CurrentMana = 0;
            }
            else
            {
                player.CurrentMana = player.MaxMana;
            }

            manaBar.SetMana(player.CurrentMana);
            tempForce = 0;
        } 
        
        if(value < 0)
        {
            animator.SetBool("forca", true);
        }
    }

    public void PlayForceSound()
    {
        if(!(forceSound.isPlaying))
        {
            forceSound.pitch = Random.Range(0.9f, 1.1f);
            forceSound.volume = 1;
            forceSound.Play();
        }
    }

    public void StopForceSound() => forceSound.Stop();
    

    public void PlayLockSound()
    {
        if(!(lockSound.isPlaying))
        {
            lockSound.pitch = Random.Range(0.75f, 1f);
            lockSound.volume = Random.Range(0.95f, 1f);
            lockSound.Play();
        }
    }

    public void PlayAtirarSound()
    {
        if(!(atirarSound.isPlaying))
        {
            atirarSound.pitch = Random.Range(0.9f, 1.5f);
            atirarSound.volume = Random.Range(0.65f, 1f);
            atirarSound.Play();
        }
    }

    public Vector3 GetMousePos()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return mousePos;
    }

    public bool isMove()
    {
        return player.isMove();
    }
}

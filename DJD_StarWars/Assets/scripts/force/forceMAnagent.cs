using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forceMAnagent : MonoBehaviour
{
    
    //Variables for throw
    [SerializeField]
    private float timeForThrow = 0.025f;

    [SerializeField]
    private float speedThorw = 110;



    Player player;
    ManaUiPlayer manaBar;


    [SerializeField]
    private float tempForRecover = 0;
    private float tempRecover;

    float tempForce = 0;

    //effect
    [SerializeField]
    private Image image;
    

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>(); 
        }

        manaBar = FindObjectOfType<ManaUiPlayer>(); 
    }


    void Update()
    {
        if(!(Input.GetMouseButton(1)) && !(Input.GetMouseButton(0)))
        {
            if(tempRecover <= 0 && getCurrentMana() != 100)  //
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
            player.CurrentMana += value;
            manaBar.SetMana(player.CurrentMana);
            tempForce = 0;
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

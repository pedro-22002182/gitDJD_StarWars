using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    float tempForce = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>(); 
        }

        manaBar = FindObjectOfType<ManaUiPlayer>(); 
    }


    public float getTempRecover() => tempForRecover;

    public float getSpeedThorw() => speedThorw;

    public float getTimeForThrow() => timeForThrow;

    public int getCurrentMana() => player.CurrentMana;
    
    public void ChangeMana(int value)
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
}

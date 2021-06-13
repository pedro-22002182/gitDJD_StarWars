using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDead : MonoBehaviour
{

    [SerializeField]
    private Transform spawnCaixa;



    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if(collision.gameObject.name == "player")
        {   
            Player character = collision.GetComponent<Player>();
            character.takeDamage(1); //character.getVidaMax()
            character.GotoSafe();
        }

        if(collision.gameObject.tag == "caixa")
        {   
            Instantiate(collision.gameObject, spawnCaixa.position, collision.transform.rotation);
            Destroy(collision.gameObject);
        }   

        if(collision.gameObject.tag == "enemy")
        {   
            PlayerPrefs.SetInt("mortes", PlayerPrefs.GetInt("mortes") + 1);
            Destroy(collision.gameObject);
        }
    }
}

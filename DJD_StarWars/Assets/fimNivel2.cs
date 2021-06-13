using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fimNivel2 : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "player")
        {
            PlayerPrefs.SetInt("nivel", 3);
            PlayerPrefs.SetInt("mortesAntigas", PlayerPrefs.GetInt("mortes"));
            
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("menu");
        }
        
    }
}

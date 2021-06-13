using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour
{
    public void StartGame()
    {
        //GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("niveis");
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("nivel", 1);
        PlayerPrefs.SetInt("mortes", 0);
        PlayerPrefs.SetInt("mortesAntigas", 0);
        //StartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}

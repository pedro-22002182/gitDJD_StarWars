using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseControl : MonoBehaviour
{
    [SerializeField]
    private GameObject InterfacePause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(InterfacePause.active == false)
            {
                InterfacePause.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                InterfacePause.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void RecomecarNivel()
    {
        Time.timeScale = 1;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel(SceneManager.GetActiveScene().name);
    }

    public void menuNiveis()
    {
        Time.timeScale = 1;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("niveis");
    }
}

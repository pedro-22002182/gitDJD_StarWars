using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameMng : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMainMenu(float time)
    {
        StartCoroutine(BackToMainMenuCR(time));
    }

    IEnumerator BackToMainMenuCR(float time)
    {
        yield return new WaitForSeconds(time); 

        SceneManager.LoadScene("menu");
    }
}

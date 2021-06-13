using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{

    public bool comecarFim {set; get;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(comecarFim == true)
        {
            StartCoroutine(IrParaFim());
        }
    }

    IEnumerator IrParaFim()
    {
        yield return new WaitForSeconds(2f);

        if(PlayerPrefs.GetInt("mortes") <= 7)
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("fim1");
        
        else
        {
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("fim2");
        }
            
        
        PlayerPrefs.SetInt("nivel", PlayerPrefs.GetInt("nivel") + 1);
        
    }
}

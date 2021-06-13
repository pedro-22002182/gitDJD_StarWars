using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class niveisControl : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> butsNiveis;

    private int nivelAtual;


    // Start is called before the first frame update
    void Start()
    {
        atualizarNiveis();
    }

    public void atualizarNiveis()
    {
        if(PlayerPrefs.GetInt("nivel") == 0)
        {
            PlayerPrefs.SetInt("nivel", 1);
        }

        nivelAtual = PlayerPrefs.GetInt("nivel");

        for(int i = 0; i < butsNiveis.Count; i++)
        {
            if(PlayerPrefs.GetInt("nivel") == i+1)
            {
                butsNiveis[i].gameObject.GetComponent<Button>().interactable = true;
                butsNiveis[i].gameObject.GetComponent<Image>().color = new Color(255,255,255);
            }
            else
            {
                //ja passou o nivel
                if(PlayerPrefs.GetInt("nivel") > i+1)
                {
                    butsNiveis[i].gameObject.GetComponent<Image>().color = new Color(234,225,156);
                }
                else //nao passou
                {
                    butsNiveis[i].gameObject.GetComponent<Image>().color = new Color(140,140,140);
                }

                butsNiveis[i].gameObject.GetComponent<Button>().interactable = false;
            }

        }
    }
    public void Nivel1()
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("introducao nivel1");
    }

    public void Nivel2()
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("nivel2");
    }

    public void Nivel3()
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("nivel3");
    }

    public void menuInicial()
    {
        //GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("menu");
    }
}

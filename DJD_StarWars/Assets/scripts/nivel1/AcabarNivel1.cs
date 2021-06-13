using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class AcabarNivel1 : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialFinal;

    private Volume v;
    private Vignette vignette;

    [SerializeField]
    private float valor;

    private float valorAtual;

    // Start is called before the first frame update
    void Start()
    {
        v = GetComponent<Volume>();
        v.profile.TryGet(out vignette);

        vignette.intensity.value = 0.66f;
        valorAtual = vignette.intensity.value;
    }

    // Update is called once per frame
    void Update()
    {

        if(valor < valorAtual)
        {
            valorAtual -= 0.00015f;
        }

        vignette.intensity.value = valorAtual;

        if(valor >= valorAtual && dialFinal.checkAcabou())
        {
            PlayerPrefs.SetInt("mortes", 0);
            PlayerPrefs.SetInt("mortesAntigas", PlayerPrefs.GetInt("mortes"));

            PlayerPrefs.SetInt("nivel", 2);
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("menu");
        }
    }
}

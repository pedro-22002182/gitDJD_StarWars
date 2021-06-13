using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostDark : MonoBehaviour
{
    private Volume v;
    private Vignette vignette;

    [SerializeField]
    private float valor;

    [SerializeField]
    private float valorMax = 0.3f;

    [SerializeField]
    private int numInimigos;

    private float valorAtual;


    [SerializeField]
    private bool nivel1;

    // Start is called before the first frame update
    void Start()
    {
        v = GetComponent<Volume>();
        v.profile.TryGet(out vignette);

        valor = 0;
        vignette.intensity.value = 0;

        numInimigos += PlayerPrefs.GetInt("mortes");

        //para dar uma nocao das mortes anteriores do jogador
        for(int i = 0; i < PlayerPrefs.GetInt("mortes"); i++)
        {
            valorAtual += valorMax/numInimigos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(valorAtual > valor && valor < valorMax)
        {
            valor += 0.001f;
        }


        if(valor < 0)
        {
            valor = 0;
        }

        vignette.intensity.value = valor;
    }

    public float GetMaxValor()
    {
        return valorMax;
    }

    public float GetValor()
    {
        return valor;
    }

    public void MaisUmaMorte()
    {
        //nivel 1 é especial, pois dá reset ás mortes e apenas mostra ao jogador a situacao
        if(nivel1 && valorAtual == 0)
        {
            valorAtual = 0.15f;
        }
        else if(nivel1)
        {
            valorAtual += 0.66f/numInimigos;
        }
        else
        {
            valorAtual += valorMax/numInimigos;
        }
    }
}

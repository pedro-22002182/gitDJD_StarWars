using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimoBase : MonoBehaviour
{

    [SerializeField]
    private float vida = 100;

    [SerializeField]
    private float dano = 10;

    // Start is called before the first frame update
    void Start()
    {
        // fazer dps as cenas quando gajo morre
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float dano)
    {
        vida -= dano;
        if(vida < 0)
        {
            vida = 0;
        }
    }

    public float getDano()
    {
        return dano;
    }
}

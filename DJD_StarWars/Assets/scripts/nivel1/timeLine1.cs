using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeLine1 : MonoBehaviour
{
    [SerializeField]
    private Dialogue conversa;

    [SerializeField]
    private GameObject introCenamatic;

    [SerializeField]
    private GameObject cameraNormal;

    private int umaVez;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(conversa.checkAcabou() && umaVez == 0)
        {
            introCenamatic.SetActive(true);
            cameraNormal.SetActive(true);
            umaVez += 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject[] hearts;
    inimigoBoss boss;
    

    // Update is called once per frame
    void Update()
    {
        if(boss == null)
            boss = FindObjectOfType<inimigoBoss>(); 
        
        int nHearts = 0;

        if(boss != null)
        {
            nHearts = boss.nHearts;
        }

        for(int i = 0; i < nHearts; i++)
        {
            hearts[i].SetActive(true);
        }

        for(int i = nHearts; i < hearts.Length; i++)
        {
            hearts[i].SetActive(false);
        }
    }
}

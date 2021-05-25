using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeLine2 : MonoBehaviour
{

    [SerializeField]
    private GameObject cenamatic;

    private int umaVez;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player" && umaVez == 0)
        {
            cenamatic.SetActive(true);
            umaVez += 1;
        }
    }
}

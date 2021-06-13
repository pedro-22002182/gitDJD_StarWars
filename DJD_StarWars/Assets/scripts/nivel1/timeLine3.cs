using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeLine3 : MonoBehaviour
{

    [SerializeField]
    private Dialogue conversa;

    [SerializeField]
    private GameObject cenamatic;

    [SerializeField]
    private GameObject mudancaCam;

    private int umaVez;


    private float tempImovel;
    
    private Player player;

    private float tempConversa;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if(conversa.checkAcabou() && umaVez == 0)
        {
            cenamatic.SetActive(true);
            GameObject.Find("Music").GetComponent<controlMusicBack>().TurnFightSoung();
            umaVez += 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if(collision.gameObject.name == "player")
        {   
            mudancaCam.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    [SerializeField]
    private List<string> frases;

    [SerializeField]
    private Text texto;

    private DialogueManager dManager;
    

    // Start is called before the first frame update
    void Start()
    {
        dManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D col){

        if(col.gameObject.name == "player"){

            dManager.ShowBox(frases);
        }
    }
}

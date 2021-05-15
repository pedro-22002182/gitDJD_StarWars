using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    private DialogueManager dManager;

    [System.Serializable]
    struct DialogueItem
    {
        public string nome;
        public string text;

        public Image img;
    };
    [SerializeField]
    DialogueItem[] dialogue;
    

    private int index;
    private bool dialogActive;

    // Start is called before the first frame update
    void Start()
    {
        dManager = FindObjectOfType<DialogueManager>();
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogActive && Input.GetMouseButtonDown(0)){

            index += 1;

            if(index < dialogue.Length)
            {
                dManager.updateBox(dialogue[index].nome, dialogue[index].text, dialogue[index].img);
            }
            else
            {
                dManager.ShowBox(false);
                index = 0;
                dialogActive = false;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col){

        if(col.gameObject.name == "player"){
            
            dialogActive = true; 

            dManager.ShowBox(true);
            dManager.updateBox(dialogue[index].nome, dialogue[0].text, dialogue[0].img);
        }
    }
}

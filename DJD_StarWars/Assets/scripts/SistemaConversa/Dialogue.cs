using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    struct DialogueItem
    {
        public string nome;
        public string text;

        public Sprite img;
    };

    [SerializeField]
    DialogueItem[] dialogue;
    
    [SerializeField]
    private bool AtivarInstantaneo;

    [SerializeField]
    private bool playMusicCantina;

    
    private DialogueManager dManager;

    private int index;
    private bool dialogActive;
    private int criarUma;   
    private bool acabou;


    // Start is called before the first frame update
    void Start()
    {
        dManager = FindObjectOfType<DialogueManager>();
        index = 0;
        criarUma = 0;
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
                acabou = true;
            }

            dManager.SomClicar();
        }

        if(AtivarInstantaneo)
            ativarDialogo();
    }


    void OnTriggerEnter2D(Collider2D col){

        if(col.gameObject.name == "player")
            ativarDialogo();
    }

    public void ativarDialogo()
    {
        if(criarUma == 0)
        {
            dialogActive = true; 

            dManager.ShowBox(true);
            dManager.updateBox(dialogue[index].nome, dialogue[0].text, dialogue[0].img);

            dManager.AtivarMusicCantina = playMusicCantina;

            criarUma += 1;
        }
    }


    public bool checkAcabou()
    {
        return acabou;
    }
}

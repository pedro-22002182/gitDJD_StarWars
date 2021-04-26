using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [SerializeField]
    private GameObject dBox;

    [SerializeField]
    private Text dText;

    [SerializeField]
    private Image dImage;

    private List<string> dialog;
    private bool dialogActive;
    private int index, indexMax;


    // Start is called before the first frame update
    void Start()
    {
        dialogActive = false;
        index = 0;

        dBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogActive && Input.GetMouseButtonDown(0)){

            index += 1;

            if(index < indexMax)
            {
                dText.text = dialog[index];
            }
            else
            {
                dBox.SetActive(false); 
                index = 0;
                dialogActive = false;
            }
        }
    }

    public void ShowBox(List<string> dialogue){

        dBox.SetActive(true);
        dialogActive = true;
        
        dText.text = dialogue[index];

        indexMax = dialogue.Count;
        dialog = dialogue;
    }
}

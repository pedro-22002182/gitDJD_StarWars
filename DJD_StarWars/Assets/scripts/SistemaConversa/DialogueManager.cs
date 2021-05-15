using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    [SerializeField]
    private GameObject dBox;

    [SerializeField]
    private TextMeshProUGUI dName;

    [SerializeField]
    private TextMeshProUGUI dText;

    [SerializeField]
    private Image dImage;


    // Start is called before the first frame update
    void Start()
    {
        dBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowBox(bool acao){

        dBox.SetActive(acao);
    }

    public void updateBox(string nome, string texto, Image image)
    {
        dName.text = nome;
        dText.text = texto;
        //dImage = image;
    }
}

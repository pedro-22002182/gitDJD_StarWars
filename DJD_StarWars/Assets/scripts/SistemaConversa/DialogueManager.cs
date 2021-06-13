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

    public bool AtivarMusicCantina {get; set;}

    //Sons
    [SerializeField]
    private AudioSource clicarSound;

    // Start is called before the first frame update
    void Start()
    {
        dBox.SetActive(false);
    }

    public void ShowBox(bool acao){

        dBox.SetActive(acao);
        
        if(acao == false)
            AtivarMusicCantina = false;
    }

    public void updateBox(string nome, string texto, Sprite image)
    {
        dName.text = nome;
        dText.text = texto;
        dImage.sprite = image;
    }

    public void SomClicar()
    {
        clicarSound.pitch = Random.Range(1.3f, 1.55f);
        clicarSound.volume = Random.Range(0.65f, 0.8f);
        clicarSound.Play();
    }
}

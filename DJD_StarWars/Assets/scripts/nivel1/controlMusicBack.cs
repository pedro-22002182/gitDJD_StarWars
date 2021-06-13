using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlMusicBack : MonoBehaviour
{

    [SerializeField]
    private AudioSource MusicCantina;
    [SerializeField]
    private AudioSource MusicBackground;
    [SerializeField]
    private AudioSource MusicFight;

    [SerializeField]
    private GameObject dialogueBox;

    private DialogueManager dialMan;

    private bool playFightSoung;
    public bool GetDark {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        dialMan = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if(playFightSoung)
        {
            if(!(MusicFight.isPlaying))
            {
                MusicFight.Play();
                MusicFight.volume = 0;
            } 

            if(MusicFight.isPlaying && MusicFight.volume < 1)
                MusicFight.volume += 0.005f;

            if(GetDark)
                MusicFight.pitch = 0.35f;

            if(MusicBackground.isPlaying && MusicBackground.volume > 0)
            {
                MusicBackground.volume -= 0.005f;

                if(MusicBackground.volume <= 0.1f)
                    MusicBackground.Stop();
            }

            if(MusicCantina.isPlaying && MusicCantina.volume > 0)
            {
                MusicCantina.volume -= 0.005f;

                if(MusicCantina.volume <= 0.1f)
                    MusicCantina.Stop();
            }
        }

        else
        {
            if(dialogueBox.active == true && dialMan.AtivarMusicCantina)
            {
                if(!(MusicFight.isPlaying) && !(MusicCantina.isPlaying))
                {
                    MusicCantina.Play();
                    MusicCantina.volume = 0;
                } 

                if(MusicCantina.isPlaying && MusicCantina.volume < 1)
                    MusicCantina.volume += 0.005f;

                if(MusicBackground.isPlaying && MusicBackground.volume > 0)
                {
                    MusicBackground.volume -= 0.005f;

                    if(MusicBackground.volume <= 0.1f)
                        MusicBackground.Stop();
                }


            }
            else
            {
                if(!(MusicFight.isPlaying) && !(MusicBackground.isPlaying))
                {
                    MusicBackground.Play();
                    MusicBackground.volume = 0;
                }

                if(MusicBackground.isPlaying && MusicBackground.volume < 1)
                    MusicBackground.volume += 0.005f;

                if(MusicCantina.isPlaying && MusicCantina.volume > 0)
                {
                    MusicCantina.volume -= 0.005f;

                    if(MusicCantina.volume <= 0.1f)
                        MusicCantina.Stop();
                }
            }
        }
        
    }

    public void TurnFightSoung()
    {
        playFightSoung = true;
    }
}

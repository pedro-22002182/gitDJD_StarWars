using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startFight : MonoBehaviour
{
    [SerializeField]
    private AudioClip baterChaoSound;

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
        if(collision.gameObject.name == "player")
        {   
            GetComponent<Rigidbody2D>().gravityScale = 5;
            GameObject.Find("Music").GetComponent<controlMusicBack>().TurnFightSoung();
        }    
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "ForeGround")
        {   
            SoundManager.instance.PlaySound(baterChaoSound, 1, 0.3f);
            SoundManager.instance.PlaySound(baterChaoSound, 1, 0.25f);
        }
    }
}

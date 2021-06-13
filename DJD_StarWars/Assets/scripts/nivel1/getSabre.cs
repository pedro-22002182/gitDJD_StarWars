using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getSabre : MonoBehaviour
{
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
            Player character = collision.GetComponent<Player>();
            character.GetSabre();
            FindObjectOfType<DialogueManager>().ShowBox(false);
            Destroy(this.gameObject);
        }
    }

    private void OnMouseEnter()
    {
        GetComponent<Dialogue>().ativarDialogo();
    }
}

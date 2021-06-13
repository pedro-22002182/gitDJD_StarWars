using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class sceneFinalControl : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayableDirector>().state != PlayState.Playing && dialogo == null)
        {
            TextScene();
        }

        if(dialogo != null)
        {
            if(dialogo.checkAcabou())
            {
                TextScene();
            }
        }
    }

    public void TextScene()
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("textFinal");
    }
}

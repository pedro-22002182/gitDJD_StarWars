using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextCrawler : MonoBehaviour
{
    [SerializeField]
    private float scrollspeed = 20;

    [SerializeField]
    private float timeForChangeScene;

    [SerializeField]
    private AudioSource musicBack;

    [SerializeField]
    private string nextScene;

    [SerializeField]
    private bool fimScene;


    void Start()
    {
        if(fimScene)
        {
            TextMesh textObject = GetComponent<TextMesh>();
            textObject.text = "Mortes inocentes .. " + (PlayerPrefs.GetInt("mortes") + 2)+"/17 \nUm dos dois fins!"; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 localVectorUp = transform.TransformDirection(0,1,0);
        pos += localVectorUp * scrollspeed * Time.deltaTime;

        transform.position = pos;

        timeForChangeScene -= Time.deltaTime;
        
        if(timeForChangeScene <= 5)
            musicBack.volume = timeForChangeScene * 0.04f;

        if(timeForChangeScene <= 0)
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel(nextScene);
    }
}

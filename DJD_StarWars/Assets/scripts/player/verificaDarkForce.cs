using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class verificaDarkForce : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera vcam;

    [SerializeField]
    private bool nivel1;
    
    [SerializeField]
    private float speedZoom;

    [SerializeField]
    private Dialogue conversa;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float valorMax = GameObject.Find("Global Volume").GetComponent<PostDark>().GetMaxValor();
        float valor = GameObject.Find("Global Volume").GetComponent<PostDark>().GetValor();

        if(nivel1)
        {
            if(valor >= (valorMax / 1.7f))
            {
                StartCoroutine(Changecam(50));

                if(vcam.m_Lens.OrthographicSize <= 50)
                {
                    conversa.ativarDialogo();
                    GameObject.Find("Music").GetComponent<controlMusicBack>().GetDark = true;
                }
            }

            if(conversa.checkAcabou())
            {
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadNextLevel("nivel1Mestre");
            }
        }

        
    }
    
    IEnumerator Changecam(float valor)
    {
        yield return new WaitForSeconds(0.2f);

        while(vcam.m_Lens.OrthographicSize > valor)
        {
            vcam.m_Lens.OrthographicSize -= Time.deltaTime * speedZoom;
            yield return null;
        }
    }
}

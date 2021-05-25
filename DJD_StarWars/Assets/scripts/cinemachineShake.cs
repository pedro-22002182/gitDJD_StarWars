using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cinemachineShake : MonoBehaviour
{
    public static cinemachineShake Instance {get; private set;}

    private CinemachineVirtualCamera cameraVirtual;
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;
        cameraVirtual = GetComponent<CinemachineVirtualCamera>();
    }

    public void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinBasic = cameraVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinBasic.m_AmplitudeGain = 0;
            }
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinBasic = cameraVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinBasic.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;
    
    [SerializeField]
    private float followSpeed;

    [SerializeField]
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;

        if(followTarget != null)
        {
            Vector3 targetPos = followTarget.position;
            Vector3 error = targetPos - currentPos;

            Vector3 newPos = currentPos + error * followSpeed;

            currentPos = new Vector3(newPos.x, newPos.y, currentPos.z);
        }

        transform.position = currentPos + offset;
    }
}

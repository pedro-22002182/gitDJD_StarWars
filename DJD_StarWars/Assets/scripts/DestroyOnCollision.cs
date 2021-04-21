using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField]
    private string tag;

    [SerializeField]
    private GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((tag == collision.tag) || (tag == ""))
        {
            Destroy(gameObject);

            if (effect)
            {
                Instantiate(effect, transform.position, transform.rotation);
            }
        }
    }
}

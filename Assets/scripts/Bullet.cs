using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 3f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        HealthBehaviour healthBehaviour = collision.gameObject.GetComponent<HealthBehaviour>();

        if (healthBehaviour)
        {
            healthBehaviour.TakeDamage(20);
        }
        Destroy(gameObject, 0.5f);
    }
}

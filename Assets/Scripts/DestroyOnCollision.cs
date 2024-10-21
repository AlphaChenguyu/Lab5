using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bolts") || other.CompareTag("asteroids"))
        {
            Destroy(other.gameObject); 
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}

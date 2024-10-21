using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    private Rigidbody rb;

    public float rotationSpeed = 1f; 
    private Vector3 rotationAxis;
    private void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        rotationAxis = Random.insideUnitSphere;
        rb.angularVelocity = rotationAxis * rotationSpeed;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.angularDrag = 0f;
    }

    private void FixedUpdate()
    {
        // Apply torque for continuous rotation
        float randomX = Random.Range(-rotationSpeed, rotationSpeed);
        float randomY = Random.Range(-rotationSpeed, rotationSpeed);
        float randomZ = Random.Range(-rotationSpeed, rotationSpeed);

        rb.AddTorque(new Vector3(randomX, randomY, randomZ));
    }
}

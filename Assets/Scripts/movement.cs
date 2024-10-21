using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//Player movement and fire;
public class movement : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 10f;
    public float tiltAmount = 30f;
    // Limits for the game area (adjust based on your game scene)
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -5f;
    public float maxZ = 5f;

    public GameObject boltPrefab;
    public Transform firePoint;
    private float fireRate = 0.5f;
    private float nextFireTime = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InputManager.instance.fireAction.performed += OnFire;
    }
    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        InputManager.instance.fireAction.performed -= OnFire;
    }
    private void FixedUpdate()
    {
        // Read input value from the InputManager
        Vector2 moveInput = InputManager.instance.moveAction.ReadValue<Vector2>();

        // Calculate new velocity based on input
        Vector3 velocity = new Vector3(moveInput.x * moveSpeed, 0f, moveInput.y * moveSpeed);

        // Apply velocity directly to the Rigidbody
        rb.velocity = velocity;

        // Restrict the ship¡¯s position within the game area
        Vector3 clampedPosition = rb.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
        rb.position = clampedPosition;

        // Tilt the ship based on horizontal velocity
        float tilt = -rb.velocity.x * tiltAmount / moveSpeed;
        rb.rotation = Quaternion.Euler(0f, 0f, tilt);
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        // Check if enough time has passed since the last shot
        if (Time.time >= nextFireTime)
        {
            // Instantiate a bolt at the fire point's position and rotation
            Instantiate(boltPrefab, firePoint.position, Quaternion.Euler(90, 0, 0));
            // Update the next fire time
            nextFireTime = Time.time + fireRate;
        }
    }
}

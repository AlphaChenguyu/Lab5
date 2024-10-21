using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public InputActionAsset controls;

    public InputAction moveAction;
    public InputAction fireAction;
    private void Awake()
    {
        // Singleton pattern to ensure only one instance of InputManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Makes the InputManager persistent across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy any duplicate InputManager instances
        }
    }

    private void Start()
    {
        moveAction = controls.FindAction("Move");
        fireAction = controls.FindAction("Fire");
        if (moveAction != null)
        {
            moveAction.Enable();  // Enable the action so it can start receiving input
        }
        else
        {
            Debug.LogError("Move action not found in the InputActionAsset.");
        }
        
    }

    private void OnDisable()
    {
        // Disable the action when InputManager is disabled
        if (moveAction != null)
        {
            moveAction.Disable();
        }
    }

}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refs")]
    public Rigidbody rb;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    
    private PlayerControls controls;
    private Vector3 newVelocity;
    
    void Awake()
    {
        controls = new PlayerControls();
    }
    
    void OnEnable()
    {
        controls.Enable();
    }
    
    void OnDisable()
    {
        controls.Disable();
    }
    
    void Update()
    {
        Vector2 moveInput = controls.Default.Move.ReadValue<Vector2>();
        newVelocity = Vector3.up * rb.linearVelocity.y;
        
        newVelocity.x = moveInput.x * moveSpeed;
        newVelocity.y = moveInput.y * moveSpeed;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = transform.TransformDirection(newVelocity);
    }
}
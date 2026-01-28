using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    
    [Header("Settings")]
    public float walkSpeed = 3f;
    
    private PlayerControls controls;
    
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
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        move.y = 0;
        
        float speed = walkSpeed;
        
        Vector3 newVelocity = move * speed;
        newVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = newVelocity;
    }
}
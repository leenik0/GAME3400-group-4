using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded = true;
    
    // Reference to your input actions
    private PlayerMechanics inputActions;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new PlayerMechanics();
    }
    
    void OnEnable()
    {
        inputActions.Enable();
    }
    
    void OnDisable()
    {
        inputActions.Disable();
    }
    
    void FixedUpdate()
    {
        // Read the move input directly
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        
        rb.linearVelocity = new Vector3(moveInput.x * speed, rb.linearVelocity.y, moveInput.y * speed);
        
        Debug.Log("Move input: " + moveInput);
    }
}
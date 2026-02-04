using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    private Rigidbody rb;
    public float speed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 5f;
    public Transform cam;
    private bool isGrounded = true;

    [Header("Projectile Settings")]
    public GameObject projectile;
    public Transform firePoint;
    public float projectileSpeed = 20f;

    // Reference to your input actions
    private PlayerMechanics inputActions;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new PlayerMechanics();
        if (cam == null)
        {
            cam = Camera.main.transform;
        }

        if(firePoint == null)
        {
            firePoint = cam;
        }
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        if (inputActions.Player.Jump.triggered && isGrounded)
        {
            Jump();
        }

        if (inputActions.Player.Attack.triggered)
        {
            FireProjectile();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void FireProjectile()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = cam.forward * projectileSpeed;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.7f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    void FixedUpdate()
    {
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        bool isSprinting = inputActions.Player.Sprint.IsPressed();
        float currentSpeed = isSprinting ? sprintSpeed : speed;

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x).normalized;

        rb.linearVelocity = new Vector3(moveDirection.x * currentSpeed, rb.linearVelocity.y, moveDirection.z * currentSpeed);
    }
}
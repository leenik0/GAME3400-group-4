using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 2f;
    public float sprintSpeed = 4f;
    public Transform cam;
    private PlayerMechanics inputActions;

    [Header("Jump & Gravity")]
    public float jumpHeight = 1f;
    public float gravity = -9.81f;
    private float verticalVelocity = 0f;

    private bool _isSprinting;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputActions = new PlayerMechanics();
        if (cam == null)
            cam = Camera.main.transform;
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Sprint.performed += ctx => _isSprinting = ctx.ReadValueAsButton();
        inputActions.Player.Sprint.canceled += _ => _isSprinting = false;
    }
    void OnDisable() { inputActions.Disable(); }

    void Update()
    {
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x).normalized;

        if (controller.isGrounded)
        {
            verticalVelocity = -2f;

            if (inputActions.Player.Jump.WasPerformedThisFrame())
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        var moveSpeed = _isSprinting ? sprintSpeed : speed;
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }
}
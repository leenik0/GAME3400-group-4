using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5f;
    public Transform cam;
    private PlayerMechanics inputActions;

    [Header("Jump & Gravity")]
    public float jumpHeight = 1f;
    public float gravity = -9.81f;
    private float verticalVelocity = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputActions = new PlayerMechanics();
        if (cam == null)
            cam = Camera.main.transform;
    }

    void OnEnable() { inputActions.Enable(); }
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

        Vector3 velocity = moveDirection * speed;
        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }
}
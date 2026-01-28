using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Settings")]
    public float bobSpeed = 5f;
    public float bobAmount = 0.05f;

    [Header("References")]
    public Rigidbody rb;

    private PlayerControls controls;
    private Vector3 startPosition;
    private float timer = 0f;

    void Awake()
    {
        controls = new PlayerControls();
        startPosition = transform.localPosition;
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

        if (moveInput.magnitude > 0.1f && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            timer += Time.deltaTime * bobSpeed;

            float bobOffset = Mathf.Sin(timer) * bobAmount;

            transform.localPosition = new Vector3(
                startPosition.x,
                startPosition.y + bobOffset,
                startPosition.z
            );
        }
        else
        {
            timer = 0f;
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                startPosition,
                Time.deltaTime * bobSpeed
            );
        }
    }
}

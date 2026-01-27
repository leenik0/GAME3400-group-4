using UnityEngine;
using UnityEngine.InputSystem;

public class LookScript : MonoBehaviour
{
    [Header("Inputs")]
    public Transform playerCamera;
    public PlayerControls controls;

    [Header("Settings")]
    public float mouseSensitivity = 2f;
    private float xRotation = 0f;

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

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 lookInput = controls.Default.Look.ReadValue<Vector2>();
        transform.Rotate(Vector3.up * lookInput.x * mouseSensitivity * Time.deltaTime);

        xRotation -= lookInput.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
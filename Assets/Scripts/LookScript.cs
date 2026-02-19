using UnityEngine;
using UnityEngine.InputSystem;
public class LookScript : MonoBehaviour
{
    public PlayerMechanics controls;
    [Header("Settings")]
    public float mouseSensitivity = 10f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Awake()
    {
        controls = new PlayerMechanics();
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
        Vector2 lookInput = controls.Player.Look.ReadValue<Vector2>();
        yRotation += lookInput.x * mouseSensitivity * Time.deltaTime;
        xRotation -= lookInput.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Transform cam;
    public float moveSpeed = 2.0f;
    public float lookSensitivity = 0.1f;
    public InputAction moveAction;
    public InputAction lookAction;

    private CharacterController _controller;
    private Vector2 _moveInput;
    private Vector2 _lookInput;

    private float _yaw;
    private float _pitch;
    
    private float _bob;
    private Vector3 _camInitPos;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        moveAction.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += _ => _moveInput = Vector2.zero;

        lookAction.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        lookAction.canceled += _ => _lookInput = Vector2.zero;

        _yaw = transform.eulerAngles.y;
        _pitch = cam.localEulerAngles.x;

        _camInitPos = cam.localPosition;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        HandleMove();
        HandleLook();
        HandleBob();
    }

    private void HandleMove()
    {
        var input = new Vector3(_moveInput.x, 0f, _moveInput.y);
        input = Vector3.ClampMagnitude(input, 1f);
        var vel = transform.TransformDirection(input) * moveSpeed;
        var verticalVel = _controller.isGrounded ? -2f : -20f;
        vel.y = verticalVel;
        _controller.Move(vel * Time.deltaTime);
    }

    private void HandleLook()
    {
        var d = _lookInput * lookSensitivity;
        d *= Time.deltaTime * 60f;
        _yaw += d.x;
        _pitch = Mathf.Clamp(_pitch - d.y, -80f, 80f);
        transform.rotation = Quaternion.Euler(0f, _yaw, 0f);
        cam.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }

    private void HandleBob()
    {
        var pos = _camInitPos;
        if (_moveInput.sqrMagnitude > 0.01f)
        {
            _bob += Time.deltaTime * moveSpeed * 4f;
            var offset = Mathf.Sin(_bob) * 0.05f;
            pos = _camInitPos + Vector3.up * offset;
        }
        else
        {
            _bob = 0f;
        }
        cam.localPosition = Vector3.Lerp(cam.localPosition, pos, Time.deltaTime * 10f);
    }

    public void Face(Vector3 dir)
    {
        dir.y = 0f;
        var rot = Quaternion.LookRotation(dir.normalized, Vector3.up);
        _yaw = rot.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, _yaw, 0f);
    }

    public void SetPitch(float pitch)
    {
        _pitch = Mathf.Clamp(pitch, -80f, 80f);
        _pitch = pitch;
    }
}

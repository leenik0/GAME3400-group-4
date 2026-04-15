using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AccessComputer : MonoBehaviour
{
    public GameObject popupText;
    public GameObject loginScreen;
    private PlayerMechanics playerMechanics;
    private bool canInteract = false;

    void Awake()
    {
        playerMechanics = new PlayerMechanics();
    }

    void OnEnable()
    {
        playerMechanics.Default.Interact.started += OnKeyPressed;
        playerMechanics.Enable();
    }

    void OnDisable()
    {
        playerMechanics.Default.Interact.started -= OnKeyPressed;
        playerMechanics.Disable();
    }

    void OnKeyPressed(InputAction.CallbackContext context)
    {
        Debug.Log("E pressed");
        if (canInteract)
        {
            Debug.Log("Loading Screen");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (loginScreen) { loginScreen.SetActive(true); }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        canInteract = true;
        if (popupText) popupText.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        canInteract = false;
        if (popupText) popupText.SetActive(false);
    }
}

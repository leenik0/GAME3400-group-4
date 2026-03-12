using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool canPickup;
    private GameObject pickupObject;
    public PlayerMechanics inputActions;

    void Awake()
    {
        inputActions = new PlayerMechanics();
        inputActions.Player.Equip.performed += ctx => Equip();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Equip()
    {
        if (canPickup)
        {
            Destroy(pickupObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            canPickup = true;
            pickupObject = other.gameObject;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        canPickup = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpObject : MonoBehaviour
{
    public GameObject myHands;
    private bool canPickup;
    private GameObject pickupObject;
    bool hasItem;
    public PlayerMechanics inputActions;

    void Awake()
    {
        inputActions = new PlayerMechanics();
        inputActions.Player.Interact.performed += ctx => Pickup();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        canPickup = false;
        hasItem = false;
    }

    void Pickup()
    {
        if (canPickup == true && !hasItem)
        {
            pickupObject.GetComponent<Rigidbody>().isKinematic = true;

            Collider[] colliders = pickupObject.GetComponents<Collider>();
            foreach (Collider col in colliders)
            {
                col.enabled = false;
            }

            pickupObject.transform.position = myHands.transform.position;
            pickupObject.transform.rotation = myHands.transform.rotation;
            pickupObject.transform.Rotate(-90f, 0, 0); 
            pickupObject.transform.parent = myHands.transform;
            hasItem = true;
        }
        else if (hasItem)
        {
            pickupObject.GetComponent<Rigidbody>().isKinematic = false;

            Collider[] colliders = pickupObject.GetComponents<Collider>();
            foreach (Collider col in colliders)
            {
                col.enabled = true;
            }

            pickupObject.transform.parent = null;
            hasItem = false;
            pickupObject = null;
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

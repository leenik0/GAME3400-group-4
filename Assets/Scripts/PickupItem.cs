using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool canPickup;
    private GameObject pickupObject;
    public PlayerMechanics inputActions;
    public GameObject destination;
    public FlashEffect flashEffect;
    private AudioSource audioSource;
    public AudioClip flashSFX;

    void Awake()
    {
        inputActions = new PlayerMechanics();
        inputActions.Player.Equip.performed += ctx => Equip();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(flashSFX);
            Invoke("Teleport", 2.15f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Watch")
        {
            canPickup = true;
            pickupObject = other.gameObject;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        canPickup = false;
    }

    private void Teleport()
    {
        flashEffect.TriggerFlash();
        gameObject.transform.position = destination.transform.position;
        Physics.SyncTransforms();
    }
}

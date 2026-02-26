using UnityEngine;

public class DollyTrigger : MonoBehaviour
{
    public DollyZoomEffect dollyScript;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (dollyScript == null)
        {
            dollyScript = Camera.main.GetComponent<DollyZoomEffect>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dollyScript.effectEnabled = true;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dollyScript.effectEnabled = false;
            audioSource.loop = false;
        }
    }
}

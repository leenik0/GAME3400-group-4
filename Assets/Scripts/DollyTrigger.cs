using UnityEngine;

public class DollyTrigger : MonoBehaviour
{
    public DollyZoomEffect dollyScript;

    private void Start()
    {
        if (dollyScript == null)
            dollyScript = Camera.main.GetComponent<DollyZoomEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dollyScript.effectEnabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dollyScript.effectEnabled = false;
            // dollyScript.fieldOfView = 60f;
        }
    }
}

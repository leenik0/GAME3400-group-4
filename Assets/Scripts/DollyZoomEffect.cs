using UnityEngine;

public class DollyZoomEffect : MonoBehaviour
{
    public Transform target; // object at end of hallway
    private Camera cam;
    private float initialHeight;
    public bool effectEnabled = true;

    void Start()
    {
        cam = GetComponent<Camera>();
        float distance = Vector3.Distance(transform.position, target.position);
        initialHeight = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    void LateUpdate()
    {
        if (effectEnabled && target != null)
        {
            float currentDistance = Vector3.Distance(transform.position, target.position);
            // FOV manipulation to make the hallway look like it's stretching      
            cam.fieldOfView = 2.0f * Mathf.Atan(initialHeight * 0.5f / currentDistance) * Mathf.Rad2Deg;
        }
    }
}

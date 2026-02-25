using UnityEngine;

public class DollyZoomEffect : MonoBehaviour
{
    public Transform target; // object at end of hallway
    private Camera cam;
    private float initialHeight;
    public bool effectEnabled = false;
    public float multiplier = 2.0f;
    public float transitionSpeed = 3.0f;
    private float defaultFOV;
    private float targetFOV;

    void Start()
    {
        cam = GetComponent<Camera>();
        defaultFOV = cam.fieldOfView;
        float distance = Vector3.Distance(transform.position, target.position);
        initialHeight = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    void LateUpdate()
    {
        if (effectEnabled && target != null)
        {
            float currentDistance = Vector3.Distance(transform.position, target.position);
            currentDistance = Mathf.Max(currentDistance, 0.01f);
            // FOV manipulation to make the hallway look like it's stretching      
            targetFOV = Mathf.Clamp(2.0f * Mathf.Atan(initialHeight * 0.5f * multiplier / currentDistance)
            * Mathf.Rad2Deg, 1f, 179f);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * transitionSpeed);
        }
        else
        {
            targetFOV = defaultFOV;
        }
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * transitionSpeed);
    }
}

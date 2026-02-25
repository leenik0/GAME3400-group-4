using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DollyZoomEffect : MonoBehaviour
{
    [Header("Zoom Details")]
    public Transform target; // object at end of hallway
    private Camera cam;
    private float initialHeight;
    public bool effectEnabled = false;
    public float multiplier = 2.0f;
    public float transitionSpeed = 3.0f;
    private float defaultFOV;
    private float targetFOV;

    [Header("Vignette Effects")]
    public Volume postProcessVolume;
    public float maxIntensity = 0.5f;
    public float increaseSpeed = 0.1f;
    private Vignette vignette;

    void Start()
    {
        cam = GetComponent<Camera>();
        defaultFOV = cam.fieldOfView;
        float distance = Vector3.Distance(transform.position, target.position);
        initialHeight = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        if (postProcessVolume.profile.TryGet(out vignette))
        {
            vignette.intensity.overrideState = true;
            vignette.intensity.value = 0.2f;
        }
    }

    void Update()
    {
        if (effectEnabled && target != null)
        {
            vignette.intensity.value += increaseSpeed * Time.deltaTime;
            vignette.intensity.value = Mathf.Clamp(vignette.intensity.value, 0.2f, maxIntensity);
        }
        else if (vignette.intensity.value > 0.2f)
        {
            vignette.intensity.value -= increaseSpeed * Time.deltaTime;
            vignette.intensity.value = Mathf.Clamp(vignette.intensity.value, 0.2f, maxIntensity);
        }
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

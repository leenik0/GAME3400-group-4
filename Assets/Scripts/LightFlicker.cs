using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light lightToFlicker;
    [SerializeField, Range(0f, 3f)] private float minIntensity = 0.5f;
    [SerializeField, Range(0f, 3f)] private float maxIntensity = 1.2f;
    [SerializeField, Min(0f)] private float timeBetweenIntensity = 0.1f;
    private float currentTimer;
    void Awake()
    {
        if (!lightToFlicker)
        {
            lightToFlicker = GetComponent<Light>();
        }
    }

    public void Update()
    {
        Flicker();
    }

    public void Flicker()
    {
        currentTimer += Time.deltaTime;
        if (!(currentTimer >= timeBetweenIntensity)) { return; }
        lightToFlicker.intensity = Random.Range(minIntensity, maxIntensity);
        currentTimer = 0;
    }

    private void ValidateIntensityBounds()
    {
        if (!(minIntensity > maxIntensity))
        {
            return;
        }
        (minIntensity, maxIntensity) = (maxIntensity, minIntensity);
    }
}

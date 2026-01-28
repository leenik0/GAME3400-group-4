using UnityEngine;

public class WarningLightFlasher : MonoBehaviour
{
    public enum Pattern { SingleFlash, DoubleFlash, SteadyBlink }

    [Header("Pattern")]
    public Pattern pattern = Pattern.DoubleFlash;
    public float onTime = 0.12f;
    public float offTime = 0.18f;
    public float doubleGap = 0.10f;
    public float pauseAfterSequence = 0.75f;

    [Header("Targets")]
    public Light pointLight;
    public float baseIntensity = 2.2f;

    public Renderer lensRenderer;
    public Color warningColor = new Color(1f, 0.1f, 0.1f, 1f);
    public float emissionIntensity = 4.0f;

    [Header("Audio")]
    public AudioSource alarmAudio;
    public AudioClip alarmClip;
    [Range(0f, 1f)] public float alarmVolume = 0.8f;

    [Header("Phase")]
    public float phaseOffset = 0f;

    float t;
    bool isOn;

    void OnEnable()
    {
        t = -phaseOffset;
        ApplyState(false);
    }

    void Update()
    {
        t += Time.deltaTime;

        switch (pattern)
        {
            case Pattern.SteadyBlink: RunSteadyBlink(); break;
            case Pattern.SingleFlash: RunSingleFlash(); break;
            case Pattern.DoubleFlash:
            default: RunDoubleFlash(); break;
        }
    }

    void RunSteadyBlink()
    {
        float period = Mathf.Max(0.05f, onTime + offTime);
        float m = Mathf.Repeat(t, period);
        ApplyState(m < onTime);
    }

    void RunSingleFlash()
    {
        float seq = Mathf.Max(0.05f, onTime + offTime + pauseAfterSequence);
        float m = Mathf.Repeat(t, seq);
        ApplyState(m < onTime);
    }

    void RunDoubleFlash()
    {
        float seq = Mathf.Max(0.05f, onTime + doubleGap + onTime + offTime + pauseAfterSequence);
        float m = Mathf.Repeat(t, seq);

        if (m < onTime) { ApplyState(true); return; }
        m -= onTime;

        if (m < doubleGap) { ApplyState(false); return; }
        m -= doubleGap;

        if (m < onTime) { ApplyState(true); return; }
        ApplyState(false);
    }

    void ApplyState(bool on)
    {
        if (isOn == on) return;

        // 只有从“灭 -> 亮”的瞬间播一次
        if (!isOn && on)
        {
            if (alarmAudio != null && alarmClip != null)
                alarmAudio.PlayOneShot(alarmClip, alarmVolume);
        }

        isOn = on;

        if (pointLight != null)
            pointLight.intensity = on ? baseIntensity : 0f;

        if (lensRenderer != null)
        {
            Material mat = lensRenderer.material; // instance (per object)
            mat.EnableKeyword("_EMISSION");

            if (on)
            {
                mat.color = warningColor * 0.6f;
                mat.SetColor("_EmissionColor", warningColor * emissionIntensity);
            }
            else
            {
                mat.color = new Color(0.18f, 0.18f, 0.18f, 1f);
                mat.SetColor("_EmissionColor", Color.black);
            }
        }
    }
}

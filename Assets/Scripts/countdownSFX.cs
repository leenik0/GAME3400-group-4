using UnityEngine;

using UnityEngine;

public class countdownSFX : MonoBehaviour
{
    public CountdownTimer countdown;
    public AudioClip tickSFX;

    private AudioSource audioSource;
    private int lastSecond;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastSecond = Mathf.CeilToInt(countdown.timeRemaining);
    }

    void Update()
    {
        if (countdown.timeRemaining > 0)
        {
            int currentSecond = Mathf.CeilToInt(countdown.timeRemaining);

            if (currentSecond < lastSecond)
            {
                lastSecond = currentSecond;
                audioSource.PlayOneShot(tickSFX);
            }
        }
    }
}
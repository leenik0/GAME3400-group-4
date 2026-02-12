using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AnimalSound : MonoBehaviour
{
    public float minDelay = 5f;
    public float maxDelay = 10f;

    private AudioSource audioSource;
    private float timer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer = Random.Range(minDelay, maxDelay);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);

            audioSource.Play();

            timer = Random.Range(minDelay, maxDelay) + audioSource.clip.length;
        }
    }
}

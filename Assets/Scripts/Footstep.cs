using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footstep;

    public float stepDistance = 2f; 

    private Vector3 lastPosition;
    private float distanceMoved;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, lastPosition);
        distanceMoved += distance;

        if (distanceMoved >= stepDistance)
        {
            PlayFootstep();
            distanceMoved = 0f;
        }

        lastPosition = transform.position;
    }

    void PlayFootstep()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(footstep);
    }
}
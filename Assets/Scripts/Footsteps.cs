using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioSource audioSource;
    private CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (character.isGrounded && character.velocity.magnitude > 0.1f && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}

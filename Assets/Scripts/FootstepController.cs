using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class FootstepController : MonoBehaviour
{
    public AudioClip footstepClip;
    public float moveThreshold = 0.1f;

    private CharacterController _controller;
    private AudioSource _audio;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _audio = GetComponent<AudioSource>();
        _audio.clip = footstepClip;
        _audio.loop = true;
        _audio.playOnAwake = false;
    }

    private void Update()
    {
        Vector3 velocity = _controller.velocity;
        velocity.y = 0f;

        if (velocity.magnitude > moveThreshold)
        {
            if (!_audio.isPlaying) _audio.Play();
        }
        else
        {
            if (_audio.isPlaying) _audio.Stop();
        }
    }
}
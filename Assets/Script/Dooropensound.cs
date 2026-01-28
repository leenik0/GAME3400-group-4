using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorOpenSfxOnly : MonoBehaviour
{
    [Header("Animator")]
    public Animator animator;
    public string openingStateName = "door_opening"; 

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openSfx;
    [Range(0f, 1f)] public float volume = 0.9f;

    [Header("Timing")]
    [Range(0f, 0.3f)]
    public float earlyPlayNormalizedTime = 0.05f; 

    int openingHash;
    bool hasPlayed;

    void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();

        if (!audioSource) audioSource = GetComponent<AudioSource>();
        if (!audioSource) audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;

        openingHash = Animator.StringToHash(openingStateName);
    }

    void Update()
    {
        var state = animator.GetCurrentAnimatorStateInfo(0);

        bool inOpening =
            state.shortNameHash == openingHash ||
            state.fullPathHash == openingHash;

        if (inOpening)
        {
           
            if (!hasPlayed && state.normalizedTime <= earlyPlayNormalizedTime)
            {
                if (openSfx != null)
                    audioSource.PlayOneShot(openSfx, volume);

                hasPlayed = true;
            }
        }
        else
        {
   
            hasPlayed = false;
        }
    }
}

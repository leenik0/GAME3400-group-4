using UnityEngine;

public class Foodstep : MonoBehaviour

{
    public AudioSource footstepAudio;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;

        if (isMoving)
        {
            if (!footstepAudio.isPlaying)
                footstepAudio.Play();
        }
        else
        {
            footstepAudio.Stop();
        }
    }
}

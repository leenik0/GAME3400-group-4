using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeRemaining = 900;
    private AudioSource tickSound;
    private bool timerIsRunning = false;
    private int lastSecond;

    private void Start()
    {
        tickSound = GetComponent<AudioSource>();
        timerIsRunning = true;
        lastSecond = Mathf.CeilToInt(timeRemaining);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                int currentSecond = Mathf.CeilToInt(timeRemaining);
                if (currentSecond < lastSecond)
                {
                    tickSound.Play();
                    lastSecond = currentSecond;
                }
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(0);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

using UnityEngine;
using System;

public class ClockAnimator : MonoBehaviour
{
    [Header("指针关联")]
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    [Header("设置")]
    public bool smoothSeconds = false;

    private const float hoursToDegrees = 30f;
    private const float minutesToDegrees = 6f;
    private const float secondsToDegrees = 6f;

    // 用来存储游戏刚运行时的初始旋转角度（保留你在面板里调好的站立角度）
    private Vector3 hourInitialEuler;
    private Vector3 minuteInitialEuler;
    private Vector3 secondInitialEuler;

    void Start()
    {
        // 游戏开始的第一帧，把指针当前的 X、Y、Z 角度记录下来
        if (hourHand != null) hourInitialEuler = hourHand.localEulerAngles;
        if (minuteHand != null) minuteInitialEuler = minuteHand.localEulerAngles;
        if (secondHand != null) secondInitialEuler = secondHand.localEulerAngles;
    }

    void Update()
    {
        DateTime time = DateTime.Now;

        float currentSeconds = time.Second;
        if (smoothSeconds)
        {
            currentSeconds += time.Millisecond / 1000f;
        }

        float currentMinutes = time.Minute + (currentSeconds / 60f);
        float currentHours = (time.Hour % 12) + (currentMinutes / 60f);

        // 应用旋转：使用 initialEuler 的 x 和 y 保持站立状态，只在 z 轴上减去时间角度
        if (secondHand != null)
            secondHand.localEulerAngles = new Vector3(secondInitialEuler.x, secondInitialEuler.y, secondInitialEuler.z + currentSeconds * secondsToDegrees);

        if (minuteHand != null)
            minuteHand.localEulerAngles = new Vector3(minuteInitialEuler.x, minuteInitialEuler.y, minuteInitialEuler.z + currentMinutes * minutesToDegrees);

        if (hourHand != null)
            hourHand.localEulerAngles = new Vector3(hourInitialEuler.x, hourInitialEuler.y, hourInitialEuler.z + currentHours * hoursToDegrees);
    }
}
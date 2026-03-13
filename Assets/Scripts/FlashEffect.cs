using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashEffect : MonoBehaviour
{
    public CanvasGroup flashPanel;
    public float flashDuration = 0.1f;

    public void TriggerFlash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        flashPanel.alpha = 1;

        float timer = 0f;
        while (timer < flashDuration)
        {
            timer += Time.deltaTime;
            flashPanel.alpha = Mathf.Lerp(1f, 0f, timer / flashDuration);
            yield return null;
        }

        flashPanel.alpha = 0;
    }
}

using UnityEngine;

public class StairsController : MonoBehaviour
{
    public Transform origin;
    public PlayerController player;
    public CanvasGroup overlay;
    public float fadeStart = 5f;
    public float fadeFull = 2f;
    public float flipDistance = 1.5f;
    public Vector3 flipDirection = Vector3.forward;
    
    private void Update()
    {
        var d = Vector3.Distance(player.transform.position, origin.position);
        var t = Mathf.InverseLerp(fadeStart, fadeFull, d);
        overlay.alpha = Mathf.Clamp01(t);
        
        if (d > flipDistance) return;
        player.Face(flipDirection);
    }
}

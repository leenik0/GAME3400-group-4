using UnityEngine;

public class IllusoryWall : MonoBehaviour
{
    public Transform player;
    public Vector3 visibleSide = Vector3.forward;
    public Renderer wallRenderer;
    public Collider wallCollider;

    private void LateUpdate()
    {
        if (!player) return;

        var pos = transform.position;
        var n = transform.TransformDirection(visibleSide).normalized;
        var side = Vector3.Dot(player.position - pos, n);
        var d = Vector3.Distance(player.position, pos);

        var visible = side > 0.0001f;
        wallRenderer.enabled = visible;
        wallCollider.enabled = visible;
    }
}

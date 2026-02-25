using UnityEngine;

public class IllusoryWall : MonoBehaviour
{
    public Transform player;
    public Vector3 visibleSide = Vector3.forward;

    private Renderer[] _renderers;
    private Collider[] _cols;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        _cols = GetComponentsInChildren<Collider>();
    }

    private void LateUpdate()
    {
        if (!player) return;

        var pos = transform.position;
        var n = transform.TransformDirection(visibleSide).normalized;
        var side = Vector3.Dot(player.position - pos, n);

        var visible = side > 0.0001f;
        foreach (var r in _renderers) r.enabled = visible;
        foreach (var c in _cols) c.enabled = visible;
    }
}

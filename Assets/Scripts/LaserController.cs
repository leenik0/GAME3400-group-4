using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float maxDistance;
    [SerializeField] private float aimDelay;

    private LineRenderer _line;
    private Vector3 _aimPoint;
    private Vector3 _aimVel;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 2;
        _aimPoint = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < maxDistance)
        {
            EmitLaser();
        }
        else
        {
            ClearLaser();
        }
    }

    private void EmitLaser()
    {
        _line.enabled = true;
        var desired = player.position;
        _aimPoint = Vector3.SmoothDamp(_aimPoint, desired, ref _aimVel, aimDelay);

        var origin = transform.position;
        var dir = (_aimPoint - origin).normalized;
        var end = origin + dir * maxDistance;
        if (Physics.Raycast(origin, dir, out var hit, maxDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            end = hit.point;
        }
        _line.SetPosition(0, origin);
        _line.SetPosition(1, end);
    }

    private void ClearLaser()
    {
        if (!_line.enabled) return;
        _line.enabled = false;
        _aimPoint = transform.position;
    }
}

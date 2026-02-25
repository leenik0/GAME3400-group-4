using System;
using System.Linq;
using UnityEngine;

public class IllusoryWall : MonoBehaviour
{
    public Camera cam;
    public Vector3 visibleSide = Vector3.forward;

    private Renderer[] _renderers;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        if (!cam) cam = Camera.main;
    }

    private void LateUpdate()
    {
        if (!cam) return;

        var pos = transform.position;
        var n = transform.TransformDirection(visibleSide).normalized;
        var side = Vector3.Dot(cam.transform.position - pos, n);

        foreach (var r in _renderers) r.enabled = side > 0.01f;
    }
}

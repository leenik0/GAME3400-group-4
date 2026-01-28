using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script
{
    public class HallwaySpawner : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private HallwaySegment hallwayPrefab;
        [SerializeField] private float length = 12f;

        private readonly List<HallwaySegment> _segments = new();

        private void Start()
        {
            BuildSegments();
        }

        private void Update()
        {
            if (_segments.Count < 2) return;
            var secondStart = _segments[1].transform.position;

            if (player.position.z > secondStart.z + 1f)
            {
                Recycle();
            }
        }

        private void BuildSegments()
        {
            _segments.Clear();
            var start = transform.position;
            for (int i = 0; i < 3; i++)
            {
                var pos = start + Vector3.forward * (length * i);
                var seg = Instantiate(hallwayPrefab, pos, transform.rotation);
                seg.transform.SetParent(transform, true);
                _segments.Add(seg);
            }
            LinkSegments();
        }

        private void LinkSegments()
        {
            for (int i = 0; i < _segments.Count; i++)
            {
                var current = _segments[i];
                var next = _segments[(i + 1) % _segments.Count];
                current.next = next;
            }
        }

        private void Recycle()
        {
            var oldest = _segments[0];
            var latest = _segments.Last();
            var newPos = latest.transform.position + Vector3.forward * length;
            oldest.transform.position = newPos;
            _segments.RemoveAt(0);
            _segments.Add(oldest);
            LinkSegments();
        }
    }
}
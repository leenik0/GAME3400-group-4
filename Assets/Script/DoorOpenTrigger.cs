using System;
using UnityEngine;

namespace Script
{
    [RequireComponent(typeof(Collider))]
    public class DoorOpenTrigger : MonoBehaviour
    {
        [SerializeField] private HallwaySegment owner;
        [SerializeField] private string playerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(playerTag)) return;
            if (owner == null || owner.next == null) return;
            owner.next.door.Open();
        }
    }
}
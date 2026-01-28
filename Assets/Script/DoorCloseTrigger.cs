using UnityEngine;

namespace Script
{
    [RequireComponent(typeof(Collider))]
    public class DoorCloseTrigger : MonoBehaviour
    {
        [SerializeField] private HallwaySegment owner;
        [SerializeField] private string playerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(playerTag)) return;
            if (owner == null) return;
            owner.door.Close();
        }
    }
}
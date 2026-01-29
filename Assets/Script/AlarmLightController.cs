using System;
using System.Collections;
using UnityEngine;

namespace Script
{
    public class AlarmLightController : MonoBehaviour
    {
        [SerializeField] private float interval = 0.4f;
        [SerializeField] private Light lightSrc;
        [SerializeField] private float intensity = 4.5f;

        [SerializeField] private AudioClip sfx;
        private AudioSource _audio;

        private bool _isOn;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(Flash());
        }

        private IEnumerator Flash()
        {
            while (true)
            {
                lightSrc.intensity = _isOn ? intensity : 0f;
                if (_isOn) _audio.PlayOneShot(sfx);
                yield return new WaitForSeconds(interval);
                _isOn = !_isOn;
            }
        }
    }
}
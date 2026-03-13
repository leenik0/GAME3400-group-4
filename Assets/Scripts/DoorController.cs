using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    public AudioClip sfx;
    public AudioSource audioSrc;
    public Collider col;
    public Animator doorAnim;
    public float closeAfter = 5f;
    public InputAction interact;
    
    private bool _insideRange;
    private bool _isOpen;
    private Coroutine _autoClose;

    private void OnEnable()
    {
        interact.Enable();
        interact.performed += _ =>
        {
            if (!_insideRange) return;
            ToggleDoor();
        };
    }

    private void OnDisable()
    {
        interact.Disable();
    }

    private void ToggleDoor()
    {
        _isOpen = !_isOpen;
        doorAnim.SetBool("Open", _isOpen);
        audioSrc.PlayOneShot(sfx);
        col.enabled = !_isOpen;
        if (_autoClose != null) StopCoroutine(_autoClose);
        _autoClose = _isOpen ? StartCoroutine(AutoClose()) : null;
    }

    private IEnumerator AutoClose()
    {
        yield return new WaitForSeconds(closeAfter);
        if (_isOpen) ToggleDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _insideRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _insideRange = false;
    }
}
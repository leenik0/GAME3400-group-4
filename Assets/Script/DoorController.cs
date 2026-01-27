using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float defaultSpeed = 1f;
    [SerializeField] private string openParam = "IsOpen";
    [SerializeField] private Animator animator;

    private bool _isOpen;
    private void Awake()
    {
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }
    
    public void Open() => Set(true, defaultSpeed);
    public void Open(float speed) => Set(true, speed);

    public void Close() => Set(false, defaultSpeed);
    public void Close(float speed) => Set(false, speed);

    public void Toggle() => Set(!_isOpen, defaultSpeed);
    public void Toggle(float speed) => Set(!_isOpen, speed);
    
    private void Set(bool state, float speed)
    {
        if (_isOpen == state) return;
        _isOpen = state;
        animator.speed = Mathf.Max(0, speed);
        animator.SetBool(openParam, state);
    }
}

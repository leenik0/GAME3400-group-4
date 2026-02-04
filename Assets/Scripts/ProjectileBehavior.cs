using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBehavior : MonoBehaviour
{
    public float lifetime = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OllisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

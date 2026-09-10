using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifetime = 0.5f; // Change this to match your explosion length

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
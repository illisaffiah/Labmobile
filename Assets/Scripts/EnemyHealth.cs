using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject heartPreFab;

    public void Die()
    {
        // Spawn explosion
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Spawn heart reward
        if (heartPreFab != null)
            Instantiate(heartPreFab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}

using UnityEngine;

public class HeartCollect : MonoBehaviour
{
    public int lifeAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();

        if (ph != null)
        {
            ph.AddLife(lifeAmount);
            Debug.Log("Heart Collected +1 Life");
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform otherPlatform;

    [Header("Tuning")]
    public float recycleOffSet = 0.5f; // buffer supaya tak nampak seam? i dont fucking know bro

    private float platformWidth;

    void Start()
    {
        var col = GetComponent<BoxCollider2D>();
        platformWidth = col.size.x * transform.lossyScale.x; 
    }
    // Update is called once per frame
    void Update()
    {
        float half = platformWidth / 2f;

        //Edges for platform
        float thisRightEdge = transform.position.x + half;
        float thisLeftEdge = transform.position.x - half;

        //Edges for other platforms
        float otherRightEdge  = otherPlatform.position.x + half;
        float otherLeftEdge = otherPlatform.position.x - half;

        //1) player move to right: recycle platform to the front(right side)
        
        if (player.position.x > thisRightEdge + recycleOffSet)
        {
            transform.position = new Vector3(otherRightEdge + half, transform.position.y, transform.position.z);
        }

        //2) player move to left: recycle platform to the back(left side)
        if (player.position.x < thisLeftEdge - recycleOffSet)
        {
            transform.position = new Vector3(otherLeftEdge - half, transform.position.y, transform.position.z);
        }
    }
}

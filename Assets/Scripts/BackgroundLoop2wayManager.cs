using UnityEngine;

public class BackgroundLoop2WayManager : MonoBehaviour
{
    [Header("References")]
    public Transform cam;   // Drag Main Camera
    public Transform bg1;   // Drag BG_1
    public Transform bg2;   // Drag BG_2

    [Header("Tuning")]
    public float recycleOffset = 0.2f; // small value (0.1 - 0.5)

    private float width;

    void Start()
    {
        if (!cam) cam = Camera.main.transform;

        // Get width from SpriteRenderer (bg1)
        var sr = bg1.GetComponent<SpriteRenderer>();
        width = sr.bounds.size.x;
    }

    void LateUpdate()
    {
        float half = width * 0.5f;

        float camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        float camRight = cam.position.x + camHalfWidth;
        float camLeft  = cam.position.x - camHalfWidth;

        float bg1Left  = bg1.position.x - half;
        float bg1Right = bg1.position.x + half;
        float bg2Left  = bg2.position.x - half;
        float bg2Right = bg2.position.x + half;

        Transform leftBg  = (bg1Left < bg2Left) ? bg1 : bg2;
        Transform rightBg = (bg1Right > bg2Right) ? bg1 : bg2;

        float rightEdge = rightBg.position.x + half;
        float leftEdge  = leftBg.position.x - half;

        // recycle early (using camRight)
        if (camRight > rightEdge - recycleOffset)
        {
            leftBg.position = new Vector3(rightBg.position.x + width, leftBg.position.y, leftBg.position.z);
        }
        else if (camLeft < leftEdge + recycleOffset)
        {
            rightBg.position = new Vector3(leftBg.position.x - width, rightBg.position.y, rightBg.position.z);
        }
    }
}
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // drag player

    [Header("Follow Settings")]
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 1.5f, -10f);

    [Header("Bounds")]
    public bool clampY = true;
    public float minY = -2f;
    public float maxY = 2f;

    public bool clampX = false;
    public float minX = -10f;
    public float maxX = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // Target position + offset
        Vector3 desiredPosition = target.position + offset;

        // Clamp to level bounds
        if (clampY)
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        if (clampX)
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);

        // Smooth follow (lerp)
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothedPosition;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
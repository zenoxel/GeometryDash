using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // Player
    public float offsetX = 2f;   // Camera chạy trước player
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target == null) return;
        
        float targetX = target.position.x + offsetX;
        Vector3 desired = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed);
    }
}
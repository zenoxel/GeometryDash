using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float offsetX = 2f;
    public float smoothSpeed = 0.5f;

    void LateUpdate()
    {
        if (target == null) return;
        
        float targetX = target.position.x + offsetX;
        float newX = Mathf.Lerp(transform.position.x, targetX, smoothSpeed);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
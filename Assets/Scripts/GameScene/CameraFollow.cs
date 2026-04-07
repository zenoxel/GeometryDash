using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float offsetX = 2f;
    public float smoothSpeed = 20f;

    void FixedUpdate()
    {
        if (target == null) return;
        
        float targetX = target.position.x + offsetX;
        float newX = Mathf.Lerp(transform.position.x, targetX, smoothSpeed * Time.fixedDeltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
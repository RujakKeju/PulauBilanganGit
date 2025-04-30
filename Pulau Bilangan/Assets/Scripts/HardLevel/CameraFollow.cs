using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Kepiting
    public float followSpeed = 5f;
    public float offsetAmount = 2f; // Offset untuk mengikuti arah gerakan
    public Vector3 fixedOffset = new Vector3(2f, 1f, -10f); // Offset tetap yang bisa diatur di Inspector

    private Vector3 velocity = Vector3.zero;
    private Vector3 lastMoveDirection;

    void Update()
    {
        if (target == null) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            lastMoveDirection = new Vector3(moveX, moveY, 0).normalized;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Offset dinamis berdasarkan arah gerakan terakhir
        Vector3 dynamicOffset = lastMoveDirection * offsetAmount;

        // Posisi kamera = posisi kepiting + offset tetap + offset dinamis
        Vector3 targetPosition = target.position + fixedOffset + dynamicOffset;
        targetPosition.z = transform.position.z; // Jaga Z tetap

        // Smooth damp untuk pergerakan kamera yang lebih halus
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f / followSpeed);
    }
}

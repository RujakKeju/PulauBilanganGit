using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Boundary Settings")]
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Tidak ada gravitasi di air
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // Smooth movement
    }

    void Update()
    {
        // Ambil input dari keyboard
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Flip sprite hiu berdasarkan arah gerak
        if (moveInput.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(moveInput.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
    
    void FixedUpdate()
    {
        // Hitung posisi baru
        Vector2 newPosition = rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime;

        // Batasi posisi agar tidak keluar dari area
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Pindahkan hiu ke posisi baru yang sudah dibatasi
        rb.MovePosition(newPosition);
    }
}

using UnityEngine;

public class CrabMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3.0f;
    private Vector2 moveInput;

    [Header("Zone Limit")]
    public Collider2D sandZone;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // Interpolasi untuk gerakan halus
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Ambil input gerakan
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Gerakkan kepiting dalam FixedUpdate
        Vector2 newPosition = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;

        if (IsInsideSandZone(newPosition))
        {
            rb.MovePosition(newPosition);
        }

        SmoothFollowCamera();
    }

    bool IsInsideSandZone(Vector2 position)
    {
        if (sandZone == null) return true;
        return sandZone.bounds.Contains(new Vector3(position.x, position.y, 0));
    }

    void SmoothFollowCamera()
    {
        Vector3 crabPos = transform.position;
        Vector3 camPos = mainCamera.transform.position;

        // Hanya geser kamera ke kanan jika kepiting maju
        if (crabPos.x > camPos.x)
        {
            Vector3 targetPosition = new Vector3(crabPos.x, camPos.y, camPos.z);
            mainCamera.transform.position = Vector3.SmoothDamp(camPos, targetPosition, ref velocity, 0.2f); // Smooth
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock"))
        {
            Vector2 crabPos = transform.position;
            Vector2 rockPos = collision.transform.position;

            float diffX = crabPos.x - rockPos.x;
            float diffY = crabPos.y - rockPos.y;

            SpriteRenderer crabSprite = GetComponent<SpriteRenderer>();

            if (diffY > 0.2f) // Dari bawah (harus tetap di depan)
            {
                Debug.Log("Tidak bisa masuk dari bawah.");
                crabSprite.sortingOrder = 2;
            }
            else if (diffX > 0.2f || diffX < -0.2f || diffY < -0.2f) // Dari kiri, kanan, atau atas (masuk ke belakang batu)
            {
                Debug.Log("Masuk ke belakang batu.");
                crabSprite.sortingOrder = 1; // Di belakang batu
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock"))
        {
            Debug.Log("Keluar dari batu, kembali ke depan.");
            GetComponent<SpriteRenderer>().sortingOrder = 3; // Kembali ke depan batu
        }
    }


}

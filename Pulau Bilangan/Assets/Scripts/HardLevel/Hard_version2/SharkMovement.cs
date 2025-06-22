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

    [Header("Sound Settings")]
    public AudioClip swimSound; // Tambahkan ini

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        rb.gravityScale = 0f;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        // Set clip kalau belum diset di Inspector
        if (audioSource.clip == null && swimSound != null)
        {
            audioSource.clip = swimSound;
        }

        audioSource.loop = true; // Loop suara berenang
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        // Flip sprite berdasarkan arah gerak
        if (moveInput.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(moveInput.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        // 🔊 Kontrol suara berenang
        if (moveInput.sqrMagnitude > 0.1f)
        {
            if (!audioSource.isPlaying && swimSound != null)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        rb.MovePosition(newPosition);
    }
}

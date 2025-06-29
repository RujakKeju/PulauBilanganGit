using UnityEngine;

public class CrabMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3.0f;
    private Vector2 moveInput;

    [Header("Zone Limit")]
    public Collider2D sandZone;

    public AudioSource audioSource;
    public AudioClip walkingSound;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        mainCamera = Camera.main;

        if (audioSource.clip == null && walkingSound != null)
            audioSource.clip = walkingSound;

        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        HandleFacingDirection();
        HandleWalkingSound();
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;

        if (IsInsideSandZone(newPosition))
        {
            rb.MovePosition(newPosition);
        }

        SmoothFollowCamera();
    }

    void HandleFacingDirection()
    {
        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false; // hadap kanan
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true; // hadap kiri
        }
    }

    void HandleWalkingSound()
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        if (isMoving)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Pause();
        }
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

        // Perbaikan: Kamera mengikuti dengan margin threshold agar tidak 'getar' ke kiri
        float margin = 1f;

        if (Mathf.Abs(crabPos.x - camPos.x) > margin)
        {
            Vector3 targetPosition = new Vector3(crabPos.x, camPos.y, camPos.z);
            mainCamera.transform.position = Vector3.SmoothDamp(camPos, targetPosition, ref velocity, 0.2f);
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

            if (diffY > 0.2f)
            {
                crabSprite.sortingOrder = 2;
            }
            else
            {
                crabSprite.sortingOrder = 1;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock"))
        {
            GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
    }

}

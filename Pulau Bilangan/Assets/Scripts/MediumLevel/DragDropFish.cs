using UnityEngine;
using System.Collections;

public class BetterParabolaFish : MonoBehaviour
{
    [Header("Zones")]
    [SerializeField] private Collider2D sandZone;
    [SerializeField] private Collider2D waterZone;

    [Header("Jump Settings")]
    [SerializeField] private float flightTime = 1.0f;
    [SerializeField] private float gravityScale = 1.0f;

    [Header("Swim Settings")]
    [SerializeField] private float swimSpeed = 1.5f;
    [SerializeField] private float pauseBetweenSwims = 0.5f;
    [SerializeField] private GameObject splashEffect;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    private Vector3 dragOffset;
    private bool isDragging = false;
    private bool isSwimming = false;
    private bool hasEnteredWater = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0f;
        rb.isKinematic = true;
        gameObject.layer = LayerMask.NameToLayer("Fish");

        if (IsOverWaterZone())
        {
            Debug.Log("Ikan mulai di dalam air, langsung masuk ke mode berenang.");
            hasEnteredWater = true;
            StartCoroutine(EnterWaterSlowly());
        }
    }

    void OnMouseDown()
    {
        if (isSwimming) return;

        Vector2 mousePos = GetMouseWorldPos();
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Fish"));

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            isDragging = true;
            dragOffset = transform.position - (Vector3)mousePos;
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
        }
    }

    void Update()
    {
        // Jika ikan sudah seharusnya berenang namun rigidbody masih dynamic,
        // force agar menjadi kinematic.
        if (isSwimming && !rb.isKinematic && IsOverWaterZone())
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.isKinematic = true;
        }
    }


    void OnMouseDrag()
    {
        if (!isDragging) return;
        Vector3 mousePos = GetMouseWorldPos() + dragOffset;
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    void OnMouseUp()
    {
        if (!isDragging) return;
        isDragging = false;

        if (IsOverWaterZone())
        {
            Debug.Log("Ikan langsung masuk ke air, tanpa perlu lompat parabola.");
            hasEnteredWater = true; // Set flag agar tidak trigger ulang di OnTriggerEnter2D
            StartCoroutine(EnterWaterSlowly());
        }
        else if (!sandZone.bounds.Contains(transform.position))
        {
            Debug.Log("Ikan di luar pasir, lompat ke air pakai parabola.");
            ForceJump();
        }
        else
        {
            rb.isKinematic = true;
            rb.gravityScale = 0f;
        }
    }


    bool IsOverWaterZone()
    {
        if (waterZone == null) return false;
        return waterZone.bounds.Contains(transform.position);
    }


    private void ForceJump()
    {
        rb.isKinematic = false;
        rb.gravityScale = gravityScale;

        // Lompat ke atas dulu
        Vector2 startPos = transform.position;
        Vector2 peakPos = new Vector2(startPos.x, startPos.y + 2.0f); // Naik 2 unit ke atas

        // Tentukan target akhir (air)
        Vector2 endPos = new Vector2(startPos.x, waterZone.bounds.max.y - 0.2f);

        // Waktu lompat total
        float g = Physics2D.gravity.y * rb.gravityScale;
        float peakTime = flightTime / 2;
        float totalTime = flightTime;

        // Hitung kecepatan untuk naik ke atas dulu
        float vx = 0; // Tidak ada pergerakan horizontal di awal
        float vyUp = Mathf.Abs(g) * peakTime;

        // Apply velocity ke atas
        rb.velocity = new Vector2(vx, vyUp);

        // Setelah delay, lanjut ke parabola menuju air
        StartCoroutine(JumpToWaterAfterDelay(totalTime / 2, endPos));
    }

    private IEnumerator JumpToWaterAfterDelay(float delay, Vector2 endPos)
    {
        yield return new WaitForSeconds(delay);

        Vector2 startPos = transform.position;

        float g = Physics2D.gravity.y * rb.gravityScale;
        float vx = (endPos.x - startPos.x) / flightTime;
        float vy = (endPos.y - startPos.y - 0.5f * g * flightTime * flightTime) / flightTime;

        rb.velocity = new Vector2(vx, vy);
        spriteRend.flipX = vx < 0;

        // Tambahkan delay kecil untuk memastikan ikan sudah mencapai air
        yield return new WaitForSeconds(0.5f);

        // Jika ikan sudah berada di area waterZone, force state ke kinematic dan mulai berenang
        if (IsOverWaterZone())
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.isKinematic = true;
            isSwimming = true;
            StartCoroutine(SwimAround());
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WaterZone") && !hasEnteredWater)
        {
            hasEnteredWater = true;
            StartCoroutine(EnterWaterSlowly());
        }
    }



    IEnumerator EnterWaterSlowly()
    {

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0.2f;

        while (transform.position.y > waterZone.bounds.max.y - 0.3f)
        {
            yield return null;
        }

        if (splashEffect)
        {
            Instantiate(splashEffect, transform.position, Quaternion.identity);
        }

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        rb.isKinematic = true;
        isSwimming = true;


        StartCoroutine(SwimAround());
    }


    IEnumerator SwimAround()
    {

        while (isSwimming)
        {
            float targetX = Random.Range(waterZone.bounds.min.x + 0.3f, waterZone.bounds.max.x - 0.3f);
            float targetY = Random.Range(waterZone.bounds.min.y + 0.3f, waterZone.bounds.max.y - 0.3f);
            Vector3 targetPos = new Vector3(targetX, targetY, transform.position.z);

            Debug.Log(gameObject.name + " berenang ke: " + targetPos);

            spriteRend.flipX = targetPos.x < transform.position.x;

            while (Vector2.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, swimSpeed * Time.deltaTime);
                yield return null;
            }


            yield return new WaitForSeconds(pauseBetweenSwims);
        }

    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}

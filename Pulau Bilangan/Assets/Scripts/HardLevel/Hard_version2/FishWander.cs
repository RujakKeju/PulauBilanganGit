using UnityEngine;

public class FishWander : MonoBehaviour
{
    public Vector2 wanderAreaSize = new Vector2(5f, 3f); // ukuran area wander (lebar x tinggi)
    public float speed = 1f;
    public float changeDirectionInterval = 2f; // tiap 2 detik ganti arah

    private Vector2 wanderDirection;
    private Vector2 startPosition;
    private float timer;

    void Start()
    {
        startPosition = transform.position;
        ChooseNewDirection();
        timer = changeDirectionInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            ChooseNewDirection();
            timer = changeDirectionInterval;
        }

        Vector2 newPos = (Vector2)transform.position + wanderDirection * speed * Time.deltaTime;

        // Batasi posisi agar tetap di dalam area
        if (Mathf.Abs(newPos.x - startPosition.x) > wanderAreaSize.x / 2)
        {
            wanderDirection.x = -wanderDirection.x; // balik arah X
            newPos.x = Mathf.Clamp(newPos.x, startPosition.x - wanderAreaSize.x / 2, startPosition.x + wanderAreaSize.x / 2);
        }
        if (Mathf.Abs(newPos.y - startPosition.y) > wanderAreaSize.y / 2)
        {
            wanderDirection.y = -wanderDirection.y; // balik arah Y
            newPos.y = Mathf.Clamp(newPos.y, startPosition.y - wanderAreaSize.y / 2, startPosition.y + wanderAreaSize.y / 2);
        }

        transform.position = newPos;

        // Set rotation sesuai arah horizontal wanderDirection
        if (wanderDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // hadap kanan
        }
        else if (wanderDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // hadap kiri
        }

    }

    void ChooseNewDirection()
    {
        wanderDirection = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }
}

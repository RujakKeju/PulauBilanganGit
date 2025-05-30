using UnityEngine;

public class FishBoneFloat : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
    }
}

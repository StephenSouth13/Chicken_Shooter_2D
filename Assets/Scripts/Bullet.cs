using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;

    void Update()
    {
        // Bay theo local up (luôn đi lên trên)
        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self);

        // Tự hủy khi ra ngoài màn hình
        CheckOutOfScreen();
    }

    void CheckOutOfScreen()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chicken"))
        {
            Destroy(collision.gameObject); // ✅ Kill chicken
            Destroy(gameObject);           // ✅ Destroy bullet ngay lập tức
        }
    }
}

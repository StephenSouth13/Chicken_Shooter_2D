using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [Header("Egg Settings")]
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private float minLayTime = 3f;
    [SerializeField] private float maxLayTime = 6f;

    [Header("Chicken Settings")]
    [SerializeField] private int score;
    [SerializeField] private GameObject ChickenLegPrefab;

    private Coroutine layEggRoutine;
    private bool canLayEgg = true;

    private void Start()
    {
        if (eggPrefab != null)
            layEggRoutine = StartCoroutine(LayEggRoutine());
    }

    private IEnumerator LayEggRoutine()
    {
        while (canLayEgg)
        {
            yield return new WaitForSeconds(Random.Range(minLayTime, maxLayTime));
            Instantiate(eggPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            ScoreController.Instance.GetScore(score);

            if (ChickenLegPrefab != null)
            {
                GameObject leg = Instantiate(ChickenLegPrefab, transform.position, Quaternion.identity);

                // Gán tag nếu cần xử lý va chạm ở nơi khác
                leg.tag = "Chicken Leg";

                // Xử lý Rigidbody để tránh ảnh hưởng vật lý
                Rigidbody2D rb = leg.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.gravityScale = 1f;
                    rb.mass = 0.1f;
                }

                // Tự hủy sau vài giây
                Destroy(leg, 3f);
            }

            // Dừng coroutine trước khi hủy object
            if (layEggRoutine != null)
                StopCoroutine(layEggRoutine);

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Spawner.Instance.DereaChicken();
    }
}
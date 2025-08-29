using System.Collections;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [Header("Egg Settings")]
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private float minLayTime = 3f;
    [SerializeField] private float maxLayTime = 6f;

    [Header("Chicken Settings")]
    [SerializeField] private int score = 100;
    [SerializeField] private GameObject ChickenLegPrefab;
    [SerializeField] private GameObject PresentPrefab;

    private Coroutine layEggRoutine;
    private bool canLayEgg = true;

    public static int presentCount = 0;
    public static int maxPresents = 1;

    private void Start()
    {
        if (eggPrefab != null)
            layEggRoutine = StartCoroutine(LayEggRoutine());
    }

    private IEnumerator LayEggRoutine()
    {
        while (canLayEgg)
        {
            float delay = Random.Range(minLayTime, maxLayTime);

            int eggCount = GameObject.FindGameObjectsWithTag("Egg").Length;
            if (eggCount > 30) delay += 2f;

            yield return new WaitForSeconds(delay);
            Instantiate(eggPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            ScoreController.Instance?.GetScore(score);

            float dropChance = Random.Range(0f, 1f);

            if (dropChance < 0.2f && presentCount < maxPresents && PresentPrefab != null)
            {
                GameObject present = Instantiate(PresentPrefab, transform.position, Quaternion.identity);
                present.tag = "Present";
                presentCount++;
            }
            else if (ChickenLegPrefab != null)
            {
                GameObject leg = Instantiate(ChickenLegPrefab, transform.position, Quaternion.identity);
                leg.tag = "Chicken Leg";

                Rigidbody2D rb = leg.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.gravityScale = 1f;
                    rb.mass = 0.1f;
                }

                Destroy(leg, 3f);
            }

            if (layEggRoutine != null)
                StopCoroutine(layEggRoutine);

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Spawner.Instance?.DereaChicken();
    }
}
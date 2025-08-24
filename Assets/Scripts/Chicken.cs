using System.Collections;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [Header("Egg Settings")]
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private float minLayTime = 3f;
    [SerializeField] private float maxLayTime = 6f;

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

            // Đẻ trứng vô hạn, random
            Instantiate(eggPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            Destroy(gameObject);
    }
}

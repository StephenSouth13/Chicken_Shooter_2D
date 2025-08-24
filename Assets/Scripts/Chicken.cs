using System.Collections;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [Header("Egg Settings")]
    [SerializeField] private GameObject eggPrefab;        // Prefab quả trứng
    [SerializeField] private float minLayTime = 3f;       // Thời gian tối thiểu giữa các lần đẻ
    [SerializeField] private float maxLayTime = 6f;       // Thời gian tối đa giữa các lần đẻ
    [SerializeField] private float eggLifeTime = 10f;     // Trứng tự hủy sau X giây

    private Coroutine layEggRoutine;
    private bool canLayEgg = true; // có thể bật/tắt đẻ trứng nếu cần

    private void Start()
    {
        if (canLayEgg && eggPrefab != null)
        {
            layEggRoutine = StartCoroutine(LayEggRoutine());
        }
        else
        {
            Debug.LogWarning("⚠️ Chicken: EggPrefab chưa được gán hoặc canLayEgg = false.");
        }
    }

    private IEnumerator LayEggRoutine()
    {
        while (canLayEgg)
        {
            yield return new WaitForSeconds(Random.Range(minLayTime, maxLayTime));

            GameObject egg = Instantiate(eggPrefab, transform.position, Quaternion.identity);

            if (eggLifeTime > 0f)
                Destroy(egg, eggLifeTime);
        }
    }

    // Hàm bật/tắt đẻ trứng (nếu cần control ngoài)
    public void SetLayEgg(bool state)
    {
        canLayEgg = state;

        if (canLayEgg && layEggRoutine == null)
        {
            layEggRoutine = StartCoroutine(LayEggRoutine());
        }
        else if (!canLayEgg && layEggRoutine != null)
        {
            StopCoroutine(layEggRoutine);
            layEggRoutine = null;
        }
    }
}

using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject EggPrefab;
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject VFX;
    public static Boss instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(SpawmEgg());
        StartCoroutine(MoveBossLoop());
    }
    public void PutDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1); 
        }
    }
    IEnumerator SpawmEgg()
    {
        while (true)
        {
            Instantiate(EggPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f)); // thời gian giữa các lần bắn
        }
    }

    IEnumerator MoveBossLoop()
    {
        while (true)
        {
            yield return StartCoroutine(MoveBossToRandomPoint());
            yield return new WaitForSeconds(1f); // nghỉ giữa các lần di chuyển
        }
    }

    IEnumerator MoveBossToRandomPoint()
    {
        Vector3 targetPosition = GetRandomPoint();
        float moveSpeed = 5f;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition; // đảm bảo vị trí cuối cùng chính xác
    }

    Vector3 GetRandomPoint()
    {
        Vector3 viewportPoint = new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Camera.main.nearClipPlane);
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(viewportPoint);
        worldPoint.z = 0; // giữ boss ở mặt phẳng 2D
        return worldPoint;
    }
}
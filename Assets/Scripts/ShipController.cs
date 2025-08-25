using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
    public static ShipController Instance;

    [SerializeField] private GameObject ShipPrefab;

    void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        // Bỏ comment dòng này nếu bạn muốn ShipController tồn tại qua các scene.
        // DontDestroyOnLoad(gameObject);
    }
    else
    {
        // Nếu đã có một ShipController tồn tại, hủy bỏ cái mới này.
        Destroy(gameObject);
    }
}

    void Start()
    {
        // Sinh ra tàu ngay khi game bắt đầu.
        SpawnShip();
    }

    public void SpawnShip()
    {
        // Vị trí tàu được sinh ra (ví dụ: ở dưới đáy màn hình, giữa).
        Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 10));
        Instantiate(ShipPrefab, spawnPosition, Quaternion.identity);
    }

    // Coroutine để sinh ra tàu với độ trễ.
    public IEnumerator RespawnWithDelay()
    {
        // Chờ 2 giây sau khi tàu cũ bị phá hủy.
        yield return new WaitForSeconds(2f);
        // Sau đó, gọi phương thức SpawnShip để sinh ra tàu mới.
        SpawnShip();
    }
    public void StartRespawn()
{
    // Bắt đầu coroutine để hồi sinh tàu với độ trễ.
    StartCoroutine(RespawnWithDelay());
}
}
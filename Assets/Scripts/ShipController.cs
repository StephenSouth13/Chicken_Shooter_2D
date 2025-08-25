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

        var newShip = Instantiate(ShipPrefab, new Vector3(0, -10f, 0), Quaternion.identity); 
        Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.2f, 0));
        point.z = 0;
        StartCoroutine(MoveToShip(newShip, point));

    }


    // Coroutine để sinh ra tàu với độ trễ.
    public IEnumerator MoveToShip(GameObject ship, Vector3 point)
    {
    float timer = 0;
    Vector3 startPos = ship.transform.position;

    while (timer < 1f)
    {
        timer += Time.fixedDeltaTime;
        ship.transform.position = Vector3.Lerp(startPos, point, timer);
        yield return new WaitForFixedUpdate();
    }

        ship.transform.position = point;
    }

    
}
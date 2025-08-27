using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public static ShipController Instance;

    [SerializeField] private GameObject ShipPrefab;
    [SerializeField] private Image[] heartIcons;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        SpawnShip();
    }

    public void SpawnShip()
    {
        if (ShipPrefab == null)
        {
            Debug.LogError("❌ ShipPrefab is not assigned!");
            return;
        }

        var newShip = Instantiate(ShipPrefab, new Vector3(0, -10f, 0), Quaternion.identity);
        Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.2f, Camera.main.nearClipPlane));
        point.z = 0;

        StartCoroutine(MoveToShip(newShip, point));
        Debug.Log("🚀 Ship instantiated at: " + newShip.transform.position);

        Health health = newShip.GetComponent<Health>();
        if (health != null && heartIcons.Length > 0)
        {
            health.SetHearts(heartIcons);
        }
    }

    public IEnumerator MoveToShip(GameObject ship, Vector3 point)
    {
        float timer = 0f;
        Vector3 startPos = ship.transform.position;

        while (timer < 1f)
        {
            timer += Time.fixedDeltaTime;
            ship.transform.position = Vector3.Lerp(startPos, point, timer);
            yield return new WaitForFixedUpdate();
        }

        ship.transform.position = point;
    }

    public IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnShip();
        Debug.Log("🚀 Ship respawned after " + delay + " seconds.");
    }
}
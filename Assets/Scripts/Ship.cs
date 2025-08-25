using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    [Header("Ship Stats")]
    [SerializeField] private float Speed = 5f;

    [Header("Projectiles")]
    [SerializeField] private GameObject[] BulletList;
    [SerializeField] private int CurrentTierBullet = 0;

    [Header("Effects")]
    [SerializeField] private GameObject VFXExplosion;
    [SerializeField] private GameObject Shield;

    void Start()
    {
        // Kiểm tra xem Shield có được gán trong Inspector không.
        if (Shield != null)
        {
            Shield.SetActive(true);
            StartCoroutine(DisableShield());
        }
        else
        {
            Debug.LogWarning("Shield GameObject has not been assigned in the Inspector!");
        }
    }

    void Update()
    {
        Move();
        Fire();
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.position += direction * Speed * Time.deltaTime;

        // Giới hạn vị trí tàu trong camera
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0f, 1f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0f, 1f);
        transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && BulletList.Length > CurrentTierBullet)
        {
            // Bắn đạn theo Tier hiện tại.
            Instantiate(BulletList[CurrentTierBullet], transform.position, transform.rotation);
        }
    }

    IEnumerator DisableShield()
    {
        // Chờ 8 giây trước khi tắt Shield.
        yield return new WaitForSeconds(8);
        if (Shield != null)
        {
            Shield.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu không có Shield và va chạm với "Chicken" hoặc "Egg"
        if (Shield != null && !Shield.activeSelf && (collision.CompareTag("Chicken") || collision.CompareTag("Egg")))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
     if (gameObject.scene.isLoaded && ShipController.Instance != null)
            {
                var explosionVFX = Instantiate(VFXExplosion, transform.position, Quaternion.identity);
                Destroy(explosionVFX, 1f);

                ShipController.Instance.SpawnShip();
            }
    }

}

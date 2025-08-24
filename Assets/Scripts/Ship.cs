using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private GameObject[] BulletList;
    [SerializeField] private int CurrenTierBuller;
    [SerializeField] private GameObject VFX;
    [SerializeField] private GameObject Shield;


    void Start()
    {
        Shield.SetActive(true);
        StartCoroutine(DisableShield());
    }
    void Update()
    {
        Move();
        Fire();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, y, 0);
        transform.position += direction * Speed * Time.deltaTime;

        // Giới hạn trong camera
        Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, topLeft.x, bottomRight.x),
            Mathf.Clamp(transform.position.y, bottomRight.y, topLeft.y),
            transform.position.z
        );
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Space Input
        {
            // Bắn đạn lên theo hướng up của tàu
            Instantiate(BulletList[CurrenTierBuller], transform.position, transform.rotation);
        }
    }

    IEnumerator DisableShield()
    {
        yield return new WaitForSeconds(8);
        Shield.SetActive(false);
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Shield.activeSelf && (collision.CompareTag("Chicken") || collision.CompareTag("Egg")))
        {
            Destroy(collision.gameObject); // ✅ Kill chicken/egg khi va chạm
            Destroy(gameObject);           // ✅ Kill ship
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
        }
    }
}

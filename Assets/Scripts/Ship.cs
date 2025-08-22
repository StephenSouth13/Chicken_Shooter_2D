using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private GameObject[] BulletList; 
    [SerializeField] private int CurrenTierBuller = 0; 

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
        if (Input.GetKeyDown(KeyCode.Space)) // bắn bằng Space
        {
            Instantiate(BulletList[CurrenTierBuller], transform.position, Quaternion.identity);
        }
    }
}

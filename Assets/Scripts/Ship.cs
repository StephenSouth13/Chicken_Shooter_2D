using UnityEngine;

public class Ship : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float Speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");// Di chuyển theo trụ x
        float y = Input.GetAxis("Vertical");// Di chuyển theo trụ y

        Vector3 direction = new Vector3(x, y, 0); // Tính toán hướng di chuyển
        transform.position = direction * Speed * Time.deltaTime + transform.position; // Cập nhật vị trí của tàu
    }
}

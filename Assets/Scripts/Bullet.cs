using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] private float Speed = 10f;
    [SerializeField] private int damage = 10; // mặc định

    public void SetDamage(int value)
    {
        damage = value;
    }

    void Update()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Boss")
        {
            Boss.instance.PutDamage(damage);
            Destroy(gameObject);
        }
    }
}
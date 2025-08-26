using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;

    void Update()
    {
        // Bay theo local up (luôn đi lên trên)
        transform.Translate(Vector3.up * Speed * Time.deltaTime);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Boss")
        {
            Boss.instance.PutDamage(10);
            Destroy(gameObject);
        }
    }
}

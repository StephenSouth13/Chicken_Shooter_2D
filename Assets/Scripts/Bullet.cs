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

    void Start()
    {

    }
}

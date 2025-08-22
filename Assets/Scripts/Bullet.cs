using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;
    [SerializeField] private float DistanceDestroy = 10f;

    void Update()
    {
        // Bay lên theo local up
        transform.Translate(Vector3.up * Speed * Time.deltaTime);

        // Tự hủy khi quá xa camera
        DistanceIsDestroy();
    }

    void DistanceIsDestroy()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        if (Vector3.Distance(transform.position, cameraPos) > DistanceDestroy)
        {
            Destroy(gameObject);
        }
    }

    // Hoặc xài Unity sẵn có
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public class DestroyIfReachDistance : MonoBehaviour
{
    [SerializeField] private float maxDistance = 20f;

    private Vector3 screenCenter;

    private void Start()
    {
        // Tính tâm màn hình một lần để tránh gọi lại mỗi frame
        screenCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        screenCenter.z = 0;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, screenCenter) > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
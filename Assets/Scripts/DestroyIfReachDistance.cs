using UnityEngine;

public class DestroyIfReachDistance : MonoBehaviour
{
    [SerializeField] private float Distances;

    // Update is called once per frame
    void Update()
    {
        DestroyIfTrue();

    }
    void DestroyIfTrue()
    {
        Vector3 CenterScreen = Camera.main.ScreenToWorldPoint(Vector3.zero);
        CenterScreen.z = 0;
        if (Vector3.Distance(transform.position, CenterScreen) > Distances)
        {
            Destroy(gameObject);
        }
    }
}

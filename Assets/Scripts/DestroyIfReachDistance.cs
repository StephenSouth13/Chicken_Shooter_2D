using UnityEngine;

public class DestroyIfReachDistance : MonoBehaviour
{
    [SerializeField] private float Distances;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DestroyIfTrue();

    }
    void DestroyIfTrue()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > Distances)
        {
            Destroy(gameObject);
        }
    }
}

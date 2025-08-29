using UnityEngine;

public class BounceOnBottom : MonoBehaviour
{
    [SerializeField] private float bounceForce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BounceWall"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
            }
        }
    }
}
using UnityEngine;

public class ChickenLegBounce : MonoBehaviour
{
    [SerializeField] private float bounceForce = 3f;
    [SerializeField] private float lifetimeAfterBounce = 3f;

    private bool hasBounced = false;

    private void Update()
    {
        transform.Rotate(0f, 0f, 180f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !hasBounced)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce); // ✅ dùng linearVelocity
            }

            hasBounced = true;
            Destroy(gameObject, lifetimeAfterBounce);
        }
    }
}
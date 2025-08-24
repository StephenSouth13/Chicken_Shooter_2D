using UnityEngine;

public class EggScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private float destroyDelay = 0.5f;

    private bool isBroken = false;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_animator == null) _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check nếu trứng ra khỏi màn hình dưới thì vỡ
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (!isBroken && viewPos.y < 0.05f)
        {
            BreakEgg();
        }
    }

    private void BreakEgg()
    {
        isBroken = true;
        _animator.SetTrigger("break");
        _rb.bodyType = RigidbodyType2D.Static;
        Destroy(gameObject, destroyDelay);
    }
}

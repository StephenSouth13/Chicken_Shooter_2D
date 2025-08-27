using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class Ship : MonoBehaviour
{
    [Header("Ship Stats")]
    [SerializeField] private float Speed = 5f;

    [Header("Projectiles")]
    [SerializeField] private GameObject[] BulletList;
    [SerializeField] private int CurrentTierBullet = 0;
    [SerializeField] private int[] BulletDamageList;

    [Header("Effects")]
    [SerializeField] private GameObject VFXExplosion;
    [SerializeField] private GameObject Shield;
    [SerializeField] private int ScoreOfChickenLeg;

    private Health health;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    private bool isDying = false;

    private void Awake()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (health == null) Debug.LogError("❌ Health component not found!");
        if (spriteRenderer == null) Debug.LogError("❌ SpriteRenderer missing for blink effect!");
    }

    private void Start()
    {
        ActivateShield(5f);
    }

    private void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.position += direction * Speed * Time.deltaTime;

        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0f, 1f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0f, 1f);
        transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && BulletList.Length > CurrentTierBullet)
        {
            GameObject bullet = Instantiate(BulletList[CurrentTierBullet], transform.position, transform.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null && BulletDamageList.Length > CurrentTierBullet)
            {
                bulletScript.SetDamage(BulletDamageList[CurrentTierBullet]);
            }

            Debug.Log("🔫 Fired bullet tier: " + CurrentTierBullet);
        }
    }

    private void ActivateShield(float duration)
    {
        if (Shield != null) Shield.SetActive(true);

        isInvincible = true;
        StartCoroutine(DisableShieldAfter(duration));
        StartCoroutine(BlinkEffect(duration));
        Debug.Log("🛡️ Shield activated for " + duration + " seconds.");
    }

    private IEnumerator DisableShieldAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (Shield != null) Shield.SetActive(false);
        isInvincible = false;
        Debug.Log("🛡️ Shield disabled after " + seconds + " seconds.");
    }

    private IEnumerator BlinkEffect(float duration)
    {
        float blinkInterval = 0.2f;
        float timer = 0f;

        while (timer < duration)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        if (spriteRenderer != null) spriteRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDying) return;

        if ((collision.CompareTag("Chicken") || collision.CompareTag("Egg")) && !isInvincible)
        {
            Destroy(collision.gameObject);
            health?.TakeDamage(1);
            Debug.Log("💥 Hit by enemy. Damage taken.");
            StartCoroutine(HandleDeath());
        }
        else if ((collision.CompareTag("Chicken") || collision.CompareTag("Egg")) && isInvincible)
        {
            Debug.Log("⚠️ Collision ignored due to invincibility.");
        }
        else if (collision.CompareTag("Chicken Leg"))
        {
            Destroy(collision.gameObject);
            ScoreController.Instance?.GetScore(ScoreOfChickenLeg);
            health?.Heal(1);
            Debug.Log("🍗 Collected Chicken Leg. Healed.");
        }
    }

    private IEnumerator HandleDeath()
    {
        isDying = true;

        if (VFXExplosion != null)
        {
            var explosionVFX = Instantiate(VFXExplosion, transform.position, Quaternion.identity);
            Destroy(explosionVFX, 1f);
            Debug.Log("💥 Explosion VFX triggered.");
        }

        ShipController.Instance?.StartCoroutine(ShipController.Instance.RespawnAfterDelay(3f));
        yield return null;
        Destroy(gameObject);
    }

    public void UpgradeBullet()
    {
        if (CurrentTierBullet < BulletList.Length - 1)
        {
            CurrentTierBullet++;
            Debug.Log("🎁 Bullet upgraded to tier " + CurrentTierBullet);
        }
        else
        {
            Debug.Log("🎁 Bullet already at max tier.");
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    private int currentLives;
    private bool isDead = false;

    public static int persistentLives = 3;

    [Header("UI")]
    [SerializeField] private Image[] heartIcons;

    private void Start()
    {
        // Reset mạng nếu chưa có
        if (persistentLives <= 0 || persistentLives > maxLives)
        {
            persistentLives = maxLives;
            Debug.Log("❤️ Reset persistentLives to maxLives: " + maxLives);
        }

        currentLives = Mathf.Clamp(persistentLives, 0, maxLives);
        UpdateHeartsUI();
    }

    public void SetHearts(Image[] icons)
    {
        heartIcons = icons;
        UpdateHeartsUI();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentLives -= amount;
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);
        persistentLives = currentLives;
        UpdateHeartsUI();

        if (currentLives <= 0)
        {
            isDead = true;
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentLives += amount;
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);
        persistentLives = currentLives;
        UpdateHeartsUI();
    }

    private void UpdateHeartsUI()
    {
        if (heartIcons == null || heartIcons.Length == 0)
        {
            Debug.LogWarning("⚠️ Heart icons not assigned!");
            return;
        }

        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (heartIcons[i] != null)
            {
                heartIcons[i].enabled = i < currentLives;
            }
        }

        Debug.Log("❤️ Updating heart UI. Current lives: " + currentLives);
    }

    private void Die()
    {
        Debug.Log("☠️ Player died. Destroying ship.");
        Destroy(gameObject);
    }
}
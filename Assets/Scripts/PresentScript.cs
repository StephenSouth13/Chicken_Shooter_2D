using UnityEngine;

public class PresentScript : MonoBehaviour
{
    [SerializeField] private int scoreBonus = 1000;
    [SerializeField] private float destroyDelay = 5f;

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Ship ship = collision.GetComponent<Ship>();
            if (ship != null)
            {
                ship.UpgradeBullet();
                ScoreController.Instance?.GetScore(scoreBonus);
            }

            Destroy(gameObject);
        }
    }
}
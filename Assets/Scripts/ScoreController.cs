using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField] TMP_Text textScore;
    private int score;
    public static ScoreController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void GetScore(int value)
    {
        score += value;  // cộng dồn điểm
        textScore.text = "Score : " + score.ToString();
    }
}

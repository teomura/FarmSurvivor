using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;
    private int highscore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        highscore = PlayerPrefs.GetInt("highscore");
    }

    private void Start()
    {
        if (scoreText != null)
        {
            scoreText.SetText(" " + score);
        }
    }

    public void AddScore()
    {
        score++;
        scoreText.SetText(" " + score);
    }

    public int GetScore() => score;

    public int GetHighscore() => highscore;

    public void UpdateHighscore()
    {
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            highscore = score;
        }
    }
}
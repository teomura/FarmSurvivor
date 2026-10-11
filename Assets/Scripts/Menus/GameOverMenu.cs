using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI highscore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        score.SetText("Score: " + ScoreManager.instance.GetScore());
        ScoreManager.instance.UpdateHighscore();
        highscore.SetText("Highscore: " + ScoreManager.instance.GetHighscore());
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Hide();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Return()
    {
        Hide();
        SceneManager.LoadScene("MainMenu");
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI highscore;
    [SerializeField] private Button button1;

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameObject.FindAnyObjectByType<AudioLowPassFilter>().enabled = true;
        score.SetText("Score: " + ScoreManager.instance.GetScore());
        ScoreManager.instance.UpdateHighscore();
        highscore.SetText("Highscore: " + ScoreManager.instance.GetHighscore());

        EventSystem.current.SetSelectedGameObject(button1.gameObject);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Hide();
        GameObject.FindAnyObjectByType<AudioLowPassFilter>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Return()
    {
        Hide();
        SceneManager.LoadScene("MainMenu");
    }
}
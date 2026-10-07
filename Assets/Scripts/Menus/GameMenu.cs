using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName;
    [SerializeField] private TextMeshProUGUI highscore;

    public void Start()
    {
        highscore.SetText("Highscore\n" + ScoreManager.instance.GetHighscore());
    }

    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void Options()
    {
    }

    public void Quit()
    {
        Application.Quit();
    }
}
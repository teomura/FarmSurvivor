using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName;
    [SerializeField] private Button button1;
    [SerializeField] private TextMeshProUGUI highscore;

    [SerializeField] private OptionsMenu optionsMenu;
    private InputAction closeAction;

    public void Start()
    {
        EventSystem.current.SetSelectedGameObject(button1.gameObject);

        highscore.SetText("Top Score\n" + ScoreManager.instance.GetHighscore());
        closeAction = InputSystem.actions.FindAction("Cancel");
        closeAction.started += OnClose;
    }

    public void Play()
    {
        closeAction.started -= OnClose;
        SceneManager.LoadScene(gameSceneName);
    }

    public void Options()
    {
        optionsMenu.Show();
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnClose(InputAction.CallbackContext context)
    {
        if (optionsMenu.isActiveAndEnabled)
        {
            optionsMenu.Hide();
        }
    }
}
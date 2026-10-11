using UnityEngine;
using UnityEngine.InputSystem;

public class OptionsMenu : MonoBehaviour
{
    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
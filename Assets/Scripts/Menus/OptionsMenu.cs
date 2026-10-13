using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider firstSlider;
    [SerializeField] private AudioMixer audioMixer;
    private bool audioFilterPreviouslyEnabled;
    private InputAction onCancel;

    private void Awake()
    {
        onCancel = InputSystem.actions.FindAction("Cancel");
    }

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
    }

    public void SetSFXVolume(float level)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        audioFilterPreviouslyEnabled = GameObject.FindAnyObjectByType<AudioLowPassFilter>().enabled;
        GameObject.FindAnyObjectByType<AudioLowPassFilter>().enabled = true;

        EventSystem.current.SetSelectedGameObject(firstSlider.gameObject);
        onCancel.performed += OnCancel;
    }

    public void Hide()
    {
        onCancel.performed -= OnCancel;
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        GameObject.FindAnyObjectByType<AudioLowPassFilter>().enabled = audioFilterPreviouslyEnabled;

        GameMenu gameMenu = FindAnyObjectByType<GameMenu>();
        if (gameMenu != null)
        {
            gameMenu.SelectButton();
        }
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        Hide();
    }
}
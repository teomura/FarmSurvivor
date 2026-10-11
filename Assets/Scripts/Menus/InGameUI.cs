using UnityEngine;
using UnityEngine.InputSystem;

public class InGameUI : MonoBehaviour
{
    private InputAction pauseAction;
    [SerializeField] private OptionsMenu optionsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        pauseAction.started += OnPause;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (!optionsMenu)
        {
            return;
        }

        if (optionsMenu.isActiveAndEnabled)
        {
            optionsMenu.Hide();
        }
        else
        {
            optionsMenu.Show();
        }
    }
}
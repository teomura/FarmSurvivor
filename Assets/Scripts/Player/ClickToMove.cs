using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    [SerializeField]
    private PlayerController _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _player = FindAnyObjectByType<PlayerController>();
        if (_player == null)
        {
            Debug.LogError("Failed to find player");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)// detecting input
        {
            Vector3 mousePosition = GetComponentInChildren<Camera>().ScreenToWorldPoint(Mouse.current.position.ReadValue());//get mouse position

            Debug.Log("Floor CLicked");
            _player.UpdatePosition(mousePosition);//movement logic from player script here
        }
    }
}
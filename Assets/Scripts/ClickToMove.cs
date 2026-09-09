using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    [SerializeField]
    private PlayerController _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindAnyObjectByType<PlayerController>();
        if (_player == null)
        {
            Debug.LogError("Failed to find player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)// detecting input
        {
            Ray rayOrigin = GetComponentInChildren <Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());// cast a ray for every mouse click
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                if (hitInfo.collider.name == "Ground")
                {
                    Debug.Log("Floor CLicked");
                    _player.UpdatePosition(hitInfo.point);//movement logic from player script here
                }
            }

        }
    }
}

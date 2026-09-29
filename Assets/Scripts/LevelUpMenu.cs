using UnityEditor;
using UnityEngine;

public class LevelUpMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}

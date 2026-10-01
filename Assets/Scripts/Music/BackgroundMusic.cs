using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    void Awake()
    {
        // check if an instance of this obj already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);// delete the new duplicate obj
            return;
        }

        // set this obj as the main instance and protect it from being destroyed
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

}

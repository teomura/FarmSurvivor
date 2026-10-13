using UnityEngine;
using Image = UnityEngine.UI.Image;

public class StormController : MonoBehaviour
{
    [SerializeField] private Image stormLayer;
    [SerializeField] private float timeBetweenFlashes;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private float flashDuration;
    [SerializeField] private float stormTimerMin;
    [SerializeField] private float stormTimerMax;
    [SerializeField] private AudioClip[] thunderSFX;
    private float timer;
    private bool isStorm;
    private Color originalColor;
    private int numberOfFlashesLeft;

    private void Start()
    {
        originalColor = stormLayer.color;
        timer = Random.Range(stormTimerMin, stormTimerMax);
        numberOfFlashesLeft = Random.Range(1, numberOfFlashes);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (!isStorm)
            {
                isStorm = true;
                Flash();
            }
            else if (stormLayer.color.a == 0f)
            {
                stormLayer.color = originalColor;
                timer = timeBetweenFlashes;
                numberOfFlashesLeft--;
            }
            else if (numberOfFlashesLeft >= 0)
            {
                Flash();
            }
            else
            {
                isStorm = false;
                numberOfFlashesLeft = Random.Range(1, numberOfFlashes);
                timer = Random.Range(stormTimerMin, stormTimerMax);
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.clip = thunderSFX[Random.Range(0, thunderSFX.Length)];
                audioSource.volume = Random.Range(0.5f, 1f);
                audioSource.PlayDelayed(Random.Range(0.2f, 1f));
            }
        }
    }

    private void Flash()
    {
        Color temp = stormLayer.color;
        temp.a = 0f;
        stormLayer.color = temp;

        timer = flashDuration;
    }
}
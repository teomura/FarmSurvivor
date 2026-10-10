using UnityEngine;

public class SwipeAttack : MonoBehaviour
{
    public int damage = 1;

    public float timeToLive = 0.3f;

    private int lifesteal = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0f)
        {
            gameObject.SetActive(false);
            timeToLive = 0.3f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            return;
        }

        EnemyController controller = other.GetComponent<EnemyController>();

        if (controller != null)
        {
            controller.ChangeHealth(-damage);
            GetComponentInParent<PlayerController>().ChangeHealth(lifesteal);
        }
    }

    public void IncreaseDamage(int amount)
    {
        damage += amount;
        Debug.Log(damage);
    }

    public void IncreaseLifesteal(int amount)
    {
        lifesteal += amount;
    }

    public int GetLifesteal()
    {
        return lifesteal;
    }
}
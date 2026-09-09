using UnityEngine;

public class SwipeAttack : MonoBehaviour
{
    public int damage = 1;
    public float timeToLive = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController controller = other.GetComponent<EnemyController>();

        if (controller != null)
        {
            controller.ChangeHealth(-damage);            
        }
    }
}

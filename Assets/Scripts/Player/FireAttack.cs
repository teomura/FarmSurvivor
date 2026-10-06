using UnityEngine;

public class FireAttack : MonoBehaviour
{
    public int damage = 10;
    public int speed = 10;
    public float timeToLive = 3f;

    private int lifesteal = 0;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Launch(Vector3 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * speed;

        Destroy(gameObject, timeToLive);
    }

    // Update is called once per frame
    private void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            gameObject.SetActive(false);
            timeToLive = 3f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController controller = other.GetComponent<EnemyController>();

        if (controller != null)
        {
            controller.ChangeHealth(-damage);
            //GetComponentInParent<PlayerController>().ChangeHealth(lifesteal);
            Destroy(gameObject);
        }
    }
}
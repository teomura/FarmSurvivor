using UnityEngine;

public class FireAttack : MonoBehaviour
{
    [SerializeField] private bool isPlayerProjectile;
    public int damage = 10;
    public int speed = 10;
    public float timeToLive = 3f;

    private Rigidbody2D rb;

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
        if (other.isTrigger)
        {
            return;
        }
        if (isPlayerProjectile)
        {
            EnemyController controller = other.GetComponent<EnemyController>();

            if (controller != null)
            {
                controller.ChangeHealth(-damage);
                Destroy(gameObject);
            }
        }
        else
        {
            PlayerController controller = other.GetComponent<PlayerController>();

            if (controller != null)
            {
                controller.ChangeHealth(-damage);
                Destroy(gameObject);
            }
        }
    }
}
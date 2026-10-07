using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject expPrefab;

    public int maxHealth = 5;

    public int health
    { get { return currentHealth; } }

    private int currentHealth = 1;

    public float speed = 2f;
    public float attackSpeed = 2.0f;

    private bool canMove = true;
    private float attackTimer = 0f;
    private Vector3 direction;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");// locking on the player
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        GetComponent<SpriteRenderer>().flipX = player.transform.position.x < transform.position.x;
        direction = player.transform.position - transform.position;
        direction.Normalize();

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    // FixedUpdate has the same call rate as the physics system
    private void FixedUpdate()
    {
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            attackTimer += Time.deltaTime; //counter to attack start
            if (attackTimer >= attackSpeed)
            {
                player.ChangeHealth(-1);
                attackTimer = 0f;//reset timer
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 collisionDirection = collision.transform.position - transform.position;
        collisionDirection.Normalize();

        if (Vector3.Dot(direction, collisionDirection) > 0.95f)
        {
            canMove = false; //stopping enemies on collision
        }

        attackTimer = attackSpeed;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canMove = true;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBar.UpdateHealthBar(health, maxHealth);

        if (currentHealth == 0)
        {
            Instantiate(expPrefab, transform.position, Quaternion.identity);
            ScoreManager.instance.AddScore();
            Destroy(gameObject);
        }
    }
}
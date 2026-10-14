using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject expPrefab;
    [SerializeField] private GameObject hpPrefab;
    [SerializeField] private EnemyType enemyType;
    [Range(0, 100)][SerializeField] private int hpDropChance;

    public int maxHealth = 5;

    public int health
    { get { return currentHealth; } }

    private int currentHealth = 1;

    public float speed = 2f;
    public float attackSpeed = 2.0f;
    private int damage = 1;
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

        UpdateAttack(player);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        UpdateAttack(player);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        PrepareAttack(player);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 collisionDirection = collision.transform.position - transform.position;
        collisionDirection.Normalize();

        if (Vector3.Dot(direction, collisionDirection) > 0.95f)
        {
            canMove = false; //stopping enemies on collision
        }

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        PrepareAttack(player);
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
            Instantiate(expPrefab, transform.position - Vector3.up * 0.5f, Quaternion.identity);
            if (Random.Range(1, 100) < hpDropChance)
            {
                Instantiate(hpPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);//offset drop
            }
            ScoreManager.instance.AddScore();
            Destroy(gameObject);
        }
    }

    private void UpdateAttack(PlayerController player)
    {
        if (player != null)
        {
            attackTimer += Time.deltaTime; //counter to attack start
            if (attackTimer >= attackSpeed)
            {
                player.ChangeHealth(-damage);
                attackTimer = 0f;//reset timer
            }
        }
    }

    private void PrepareAttack(PlayerController player)
    {
        if (player != null)
        {
            attackTimer = attackSpeed;
        }
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }

    public void SetDamage(int newValue)
    {
        damage = newValue;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetHpDropChance(int newDropChance)
    {
        hpDropChance = newDropChance;
    }
}
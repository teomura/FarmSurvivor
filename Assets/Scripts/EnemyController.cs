using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 5;
    public float attackSpeed = 2.0f;
    public int health { get { return currentHealth; } }
    int currentHealth = 1;

    public float speed = 2f;
    private bool canMove = true;
    private float attackTimer = 0f;
    private GameObject player;

    [SerializeField] HealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");// locking on the player
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().flipX = player.transform.position.x < transform.position.x;

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
        canMove = false; //stopping enemies on collision
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
        Debug.Log(currentHealth + "/" + maxHealth);

        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private HealthBar xpBar;
    [SerializeField] private UnityEvent onLevelUp;

    [SerializeField] private UnityEvent onDeath;
    private InputAction moveAction;

    //public InputAction clickToMove;
    public float speed = 5.0f;

    public int maxHealth = 5;

    public int health
    { get { return currentHealth; } }

    private int currentHealth = 1;

    public int maxXp = 100;

    public int Exp
    { get { return currentXp; } }

    private int currentXp = 0;
    private int level = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        //clickToMove.Enable();
        currentHealth = maxHealth;
        //currentXp = maxXp;
        levelText.text = "Lv.1";
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        if (moveInput.magnitude != 0)
        {
            transform.Translate(Vector3.right * moveInput.x * Time.deltaTime * speed);
            transform.Translate(Vector3.up * moveInput.y * Time.deltaTime * speed);

            if (moveInput.x != 0) //change flip only on lateral input
            {
                if (moveInput.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = Vector3.one;
                }
            }

            GetComponent<Animator>().SetBool("isMoving", moveInput != Vector2.zero);
        }
        else
        {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
    }

    // FixedUpdate has the same call rate as the physics system
    private void FixedUpdate()
    {
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBar.UpdateHealthBar(health, maxHealth);

        if (currentHealth <= 0)
        {
            onDeath.Invoke();
        }
    }

    public void ChangeXp(int amount)
    {
        currentXp = Mathf.Clamp(currentXp + amount, 0, maxXp);
        xpBar.UpdateHealthBar(currentXp, maxXp);

        if (currentXp == maxXp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level = level + 1;//increase lvl
        levelText.text = "   " + level.ToString();//change level
        currentXp = 0;//reset bar
        maxXp = (int)(maxXp * 1.2f);
        xpBar.UpdateHealthBar(currentXp, maxXp); //refresh xpbar
        onLevelUp.Invoke();
    }

    public void IncreaseHealth(int amount)
    {
        maxHealth += amount;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public int GetLevel() => level;
}
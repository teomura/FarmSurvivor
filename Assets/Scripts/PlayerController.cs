using System;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    TextMeshProUGUI levelText;
    [SerializeField]
    HealthBar healthBar;

    [SerializeField]
    HealthBar xpBar;
    public InputAction moveAction;
    //public InputAction clickToMove;
    public float speed = 5.0f;


    public int maxHealth = 5;
    public int health { get { return currentHealth; } }   
    int currentHealth = 1;

    public int maxXp = 100;
    public int Exp { get { return currentXp; } }
    int currentXp = 0;
    int level = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
        //clickToMove.Enable();
        currentHealth = maxHealth;
        //currentXp = maxXp;
        levelText.text = "Level 1";
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse
        var distance = Vector3.Distance(_destination, transform.position);   

        //Keyboard
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
            _destination = transform.position;
        }
        else if (distance > 0.1f)
        {
            
            //direction = destination - source (math formula for direction)
            var direction = _destination - transform.position;
            direction.Normalize();//to avoid extralong vector - this normalizes it to 1

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
            transform.Translate(direction * speed * Time.deltaTime);//movement
            GetComponent<Animator>().SetBool("isMoving", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
    }
    public void UpdatePosition(Vector3 pos)
    {
        _destination = pos;
        _destination.z = transform.position.z;
    }

    [SerializeField]
    private Vector3 _destination;        //stores the updated target position of the player



    // FixedUpdate has the same call rate as the physics system
    private void FixedUpdate()
    {

    }


    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBar.UpdateHealthBar(health, maxHealth);
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
        levelText.text = "Level " + level.ToString();//change level
        currentXp = 0;//reset bar
        xpBar.UpdateHealthBar(currentXp, maxXp); //refresh xpbar
    }

}

using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] SwipeAttack swipe;
    public float attackSpeed = 1.0f;
    public Vector3 swipeOffset;
    public GameObject swipePrefab;
    float attackTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        bool isAttacking = attackTimer >= attackSpeed;
        GetComponent<Animator>().SetBool("isAttacking", isAttacking);
        //counter to attack start
        if (isAttacking)
        {
            attackTimer = 0f;   // reset timer
            swipe.gameObject.SetActive(true);
        }

    }
    
}

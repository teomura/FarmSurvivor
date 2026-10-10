using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private SwipeAttack swipe;
    public float attackSpeed = 1.0f;
    private float attackTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        attackTimer += Time.deltaTime;
        bool isAttacking = attackTimer >= attackSpeed;
        GetComponent<Animator>().SetBool("isAttacking", isAttacking);
        //counter to attack start
        if (isAttacking)
        {
            attackTimer = 0f;   // reset timer
            swipe.gameObject.SetActive(true);
            swipe.GetComponent<AudioSource>().Play();
        }
    }

    public SwipeAttack GetSwipeAttack()
    {
        return swipe;
    }
}
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] private bool isUnlocked = false;
    [SerializeField] private float attackSpeed = 1.0f;
    private float attackTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (isUnlocked)
        {
            attackTimer += Time.deltaTime;
            bool isAttacking = attackTimer >= attackSpeed;
            //counter to attack start
            if (isAttacking)
            {
                attackTimer = 0f;   // reset timer
            }
        }
    }

    public void IncreaseSpeed(float amount)
    {
        attackSpeed += amount;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public void Unlock()
    {
        isUnlocked = true;
    }

    public bool IsUnlocked()
    {
        return isUnlocked;
    }
}
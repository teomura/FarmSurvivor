using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] private bool isUnlocked = false;
    [SerializeField] private float attackSpeed = 1.0f;
    [SerializeField] private FireAttack projectilePrefab;
    [SerializeField] private Transform startPoint;
    [SerializeField] private int projectileNumber = 8;
    [SerializeField] private int damage = 100;
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
                Shoot();
                attackTimer = 0f;// reset timer
            }
        }
    }

    public void Shoot()
    {
        float angleStep = 360f / projectileNumber;
        float angle = 0f;

        for (int i = 0; i < projectileNumber; i++)
        {
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            FireAttack fireAttack = Instantiate(projectilePrefab, startPoint.position, startPoint.rotation);
            fireAttack.Launch(direction);
            angle += angleStep;
            fireAttack.damage = damage;
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

    public void IncreaseDamage(int amount)
    {
        damage += amount;
    }

    public int GetDamage()
    {
        return damage;
    }
}
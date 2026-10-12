using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField] private float attackSpeed = 1.0f;
    [SerializeField] private FireAttack projectilePrefab;
    [SerializeField] private Transform startPoint;
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
        //counter to attack start
        if (isAttacking)
        {
            Shoot();
            attackTimer = 0f;// reset timer
        }
    }

    public void Shoot()
    {
        FireAttack fireAttack = Instantiate(projectilePrefab, startPoint.position, startPoint.rotation);
        fireAttack.Launch(GetComponent<EnemyController>().GetDirection());

        fireAttack.damage = GetComponent<EnemyController>().GetDamage();
    }
}
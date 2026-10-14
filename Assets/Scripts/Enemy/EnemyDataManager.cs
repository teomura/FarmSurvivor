using UnityEngine;

public class EnemyDataManager : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private EnemyRangedAttack rangedAttack;
    [SerializeField] private GameObject aoeAttack;

    public void UpdateFromData(EnemyData data)
    {
        enemyController.maxHealth = data.hp;
        enemyController.speed = data.movementSpeed;
        enemyController.attackSpeed = data.attackSpeed;
        enemyController.SetDamage(data.damage);
        enemyController.SetHpDropChance(data.hpDropChance);

        if (rangedAttack != null)
        {
            rangedAttack.enabled = data.attacks.Contains(AttackType.Ranged);
        }
        if (aoeAttack != null)
        {
            aoeAttack.SetActive(data.attacks.Contains(AttackType.AOE));
        }
    }
}
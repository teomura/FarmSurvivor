using UnityEngine;

public class EnemyDataManager : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private GameObject rangedAttack;
    [SerializeField] private GameObject aoeAttack;

    public void UpdateFromData(EnemyData data)
    {
        enemyController.maxHealth = data.hp;
        enemyController.speed = data.movementSpeed;
        enemyController.attackSpeed = data.attackSpeed;
        enemyController.SetDamage(data.damage);

        if (rangedAttack != null)
        {
            rangedAttack.SetActive(data.attacks.Contains(AttackType.Ranged));
        }
        if (aoeAttack != null)
        {
            aoeAttack.SetActive(data.attacks.Contains(AttackType.AOE));
        }
    }
}
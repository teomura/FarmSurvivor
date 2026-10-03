using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeType.HP:
                int increaseAmount = GetComponent<PlayerController>().maxHealth * upgrade.modifier / 100; //calculate % of modifier
                GetComponent<PlayerController>().IncreaseHealth(increaseAmount);
                break;

            case UpgradeType.PrimaryAttackDamage:
                int increaseDamage = GetComponent<PlayerAttack>().GetSwipeAttack().damage * upgrade.modifier / 100;
                GetComponent<PlayerAttack>().GetSwipeAttack().IncreaseDamage(increaseDamage);
                break;

            case UpgradeType.PrimaryAttackSpeed:
                float increaseSpeed = GetComponent<PlayerAttack>().attackSpeed * upgrade.modifier / 100;
                GetComponent<PlayerAttack>().attackSpeed += increaseSpeed;
                break;

            case UpgradeType.SecondaryAttackDamage:
                break;

            case UpgradeType.SecondaryAttackSpeed:
                float increaseAttackSpeed = GetComponent<PlayerRangedAttack>().GetAttackSpeed() * upgrade.modifier / 100;
                GetComponent<PlayerRangedAttack>().IncreaseSpeed(increaseAttackSpeed);
                break;

            case UpgradeType.SecondaryAttackUnlock:
                GetComponent<PlayerRangedAttack>().Unlock();
                break;

            case UpgradeType.Lifesteal:
                break;

            case UpgradeType.MovementSpeed:
                float movementSpeed = GetComponent<PlayerController>().speed * upgrade.modifier / 100;
                GetComponent<PlayerController>().speed += movementSpeed;
                break;

            case UpgradeType.SlowAura:
                break;
        }
    }
}
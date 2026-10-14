using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private SwipeAttack swipeAttack;
    [SerializeField] private PlayerRangedAttack rangedAttack;

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
                int increaseAmount = playerController.maxHealth * upgrade.modifier / 100; //calculate % of modifier
                playerController.IncreaseHealth(increaseAmount);
                break;

            case UpgradeType.PrimaryAttackDamage:
                int increaseDamage = playerAttack.GetSwipeAttack().damage * upgrade.modifier / 100;
                playerAttack.GetSwipeAttack().IncreaseDamage(increaseDamage);

                break;

            case UpgradeType.PrimaryAttackSpeed:
                float increaseSpeed = playerAttack.attackSpeed * upgrade.modifier / 100;
                playerAttack.attackSpeed -= increaseSpeed;

                break;

            case UpgradeType.SecondaryAttackDamage:
                int increaseRangedAttack = rangedAttack.GetDamage() * upgrade.modifier / 100;
                rangedAttack.IncreaseDamage(increaseRangedAttack);

                break;

            case UpgradeType.SecondaryAttackSpeed:
                float increaseAttackSpeed = rangedAttack.GetAttackSpeed() * upgrade.modifier / 100;
                rangedAttack.IncreaseSpeed(-increaseAttackSpeed);

                break;

            case UpgradeType.SecondaryAttackUnlock:
                rangedAttack.Unlock();

                break;

            case UpgradeType.Lifesteal:
                swipeAttack.IncreaseLifesteal(upgrade.modifier);

                break;

            case UpgradeType.MovementSpeed:
                float movementSpeed = playerController.speed * upgrade.modifier / 100;
                playerController.speed += movementSpeed;

                break;
        }
    }

    public bool CanPerformUpgrade(Upgrade upgrade)
    {
        if (upgrade.upgradeType == UpgradeType.SecondaryAttackDamage)
        {
            return rangedAttack.IsUnlocked();
        }
        else if (upgrade.upgradeType == UpgradeType.SecondaryAttackSpeed)
        {
            bool canPerform = rangedAttack.IsUnlocked();
            bool isValidSpeed = rangedAttack.GetAttackSpeed() > 0.2f;
            return canPerform && isValidSpeed;
        }
        else if (upgrade.upgradeType == UpgradeType.PrimaryAttackSpeed)
        {
            return playerAttack.attackSpeed > 0.45f;
        }
        else if (upgrade.upgradeType == UpgradeType.MovementSpeed)
        {
            return playerController.speed < 8f;
        }

        return true;
    }
}
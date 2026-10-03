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
                Debug.Log("Get wrecked!");
                break;

            case UpgradeType.PrimaryAttackSpeed:
                float increaseSpeed = GetComponent<PlayerAttack>().attackSpeed * upgrade.modifier / 100;
                GetComponent<PlayerAttack>().attackSpeed -= increaseSpeed;
                Debug.Log("Hit!");
                break;

            case UpgradeType.SecondaryAttackDamage:
                Debug.Log("Cooked");
                break;

            case UpgradeType.SecondaryAttackSpeed:
                float increaseAttackSpeed = GetComponent<PlayerRangedAttack>().GetAttackSpeed() * upgrade.modifier / 100;
                GetComponent<PlayerRangedAttack>().IncreaseSpeed(-increaseAttackSpeed);
                Debug.Log("Bombs away");
                break;

            case UpgradeType.SecondaryAttackUnlock:
                GetComponent<PlayerRangedAttack>().Unlock();
                Debug.Log("Fire");
                break;

            case UpgradeType.Lifesteal:
                Debug.Log("Got your health");
                break;

            case UpgradeType.MovementSpeed:
                float movementSpeed = GetComponent<PlayerController>().speed * upgrade.modifier / 100;
                GetComponent<PlayerController>().speed += movementSpeed;
                Debug.Log("Flash");
                break;

            case UpgradeType.SlowAura:
                Debug.Log("Freeze");
                break;
        }
    }

    public bool CanPerformUpgrade(Upgrade upgrade)
    {
        if (upgrade.upgradeType == UpgradeType.SecondaryAttackDamage)
        {
            return GetComponent<PlayerRangedAttack>().IsUnlocked();
        }
        else if (upgrade.upgradeType == UpgradeType.SecondaryAttackSpeed)
        {
            bool canPerform = GetComponent<PlayerRangedAttack>().IsUnlocked();
            bool isValidSpeed = GetComponent<PlayerRangedAttack>().GetAttackSpeed() > 0.2f;
            return canPerform && isValidSpeed;
        }
        else if (upgrade.upgradeType == UpgradeType.PrimaryAttackSpeed)
        {
            return GetComponent<PlayerAttack>().attackSpeed > 0.45f;
        }
        else if (upgrade.upgradeType == UpgradeType.MovementSpeed)
        {
            return GetComponent<PlayerController>().speed < 8f;
        }

        return true;
    }
}
using UnityEngine;

public enum UpgradeType 
{
    HP,
    MovementSpeed,

    PrimaryAttackUnlock,
    PrimaryAttackDamage,
    PrimaryAttackSpeed,

    SecondaryAttackUnlock,
    SecondaryAttackDamage,
    SecondaryAttackSpeed,

    Lifesteal,
    SlowAura,
}

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrades/New Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public UpgradeType upgradeType;
    public int modifier;

}

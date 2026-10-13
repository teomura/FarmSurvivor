using UnityEngine;

public enum UpgradeType
{
    HP,
    MovementSpeed,

    PrimaryAttackDamage,
    PrimaryAttackSpeed,

    SecondaryAttackUnlock,
    SecondaryAttackDamage,
    SecondaryAttackSpeed,

    Lifesteal,
}

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrades/New Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public UpgradeType upgradeType;
    public int modifier;

    public Sprite image;
    public string description;
}
using UnityEngine;
using System.Collections.Generic;

public enum EnemyType
{
    Zombie,
    Skeleton,
    Warlock,
    Ghoul
}

public enum AttackType
{
    Melee,
    Ranged,
    AOE
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/New EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public EnemyType enemyType;
    public int requiredLevel;
    public int hp;
    public float movementSpeed;
    public float attackSpeed;
    public int damage;
    public List<AttackType> attacks = new();
}
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "EnemyInfo Scriptable")]
public class EnemyInfo : ScriptableObject
{
    public float health;
    public float moveSpeed;
    public float damage;
    public float attackRange;
    public float attackDelay;
}

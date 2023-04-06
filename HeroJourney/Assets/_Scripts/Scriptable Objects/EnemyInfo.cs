using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "EnemyInfo Scriptable")]
public class EnemyInfo : ScriptableObject
{
    public float health;
    public float moveSpeed;
    public float damage;
    public Vector3 attackOffset;
    public float attackRange;
    public float attackDelay;
}

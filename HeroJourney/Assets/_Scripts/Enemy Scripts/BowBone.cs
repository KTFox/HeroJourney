using UnityEngine;

public class BowBone : PatrollingBehaviour
{
    [Header("Bow Set Up")]
    [SerializeField] GameObject arrow;
    [SerializeField] Transform arrowPos;

    public override void Attack()
    {
        if (!beAttacked)
        {
            Instantiate(arrow, arrowPos.position, Quaternion.identity);
        }
    }
}

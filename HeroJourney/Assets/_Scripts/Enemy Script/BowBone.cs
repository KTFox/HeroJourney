using UnityEngine;

public class BowBone : PatrolEnemy
{
    [Header("Bow Set Up")]
    [SerializeField] GameObject arrow;
    [SerializeField] Transform arrowPos;

    public override void Attack() //This method will be call in animation event
    {
        if (!beAttacked)
        {
            Instantiate(arrow, arrowPos.position, Quaternion.identity);
        }
    }
}

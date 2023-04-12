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

    public override void CheckAnimationState()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Being attacekd state
        if (stateInfo.IsName("BowBone_BeAttacked") && stateInfo.normalizedTime < 1.0f)
        {
            beAttacked = true;
        }
        else { beAttacked = false; }

        // Attacking state
        if (stateInfo.IsName("BowBone_Attack") && stateInfo.normalizedTime < 1.0f)
        {
            isAttacking = true;
        }
        else { isAttacking = false; }
    }
}

using UnityEngine;

public class ScimitarBone : PatrolEnemy
{
    public override void Attack() //This method will be called in animation event
    {
        if (!beAttacked)
        {
            Vector3 pos = transform.position;
            pos += transform.right * attackOffset.x;
            pos += transform.up * attackOffset.y;

            Collider2D hitPlayer = Physics2D.OverlapCircle(pos, attackRange, LayerMask.GetMask("Player"));

            if (hitPlayer != null)
            {
                hitPlayer.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
            }
        }
    }

    public override void CheckAnimationState()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Being attacekd state
        if (stateInfo.IsName("ScimitarBone_BeAttacked") && stateInfo.normalizedTime < 1.0f)
        {
            beAttacked = true;
        }
        else { beAttacked = false; }

        // Attacking state
        if (stateInfo.IsName("BoneScimitar_Attack") && stateInfo.normalizedTime < 1.0f)
        {
            isAttacking = true;
        }
        else { isAttacking = false; }
    }
}

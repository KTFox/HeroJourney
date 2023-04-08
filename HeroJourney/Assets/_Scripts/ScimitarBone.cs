using UnityEngine;

public class ScimitarBone : PatrollingBehaviour
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
}

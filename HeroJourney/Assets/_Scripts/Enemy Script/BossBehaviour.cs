using UnityEngine;

public class BossBehaviour : StateMachineBehaviour
{
    [SerializeField] float moveSpeed;
    private float attackRange;

    private Transform player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackRange = GameObject.Find("Boss").GetComponent<Boss>().attackRange;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Boss>().LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, moveSpeed * Time.deltaTime);

        float distance = Vector2.Distance(animator.transform.position, target);
        if (distance <= attackRange)
        {
            animator.SetTrigger("attack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
    }
}

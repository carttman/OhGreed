using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class MoveState : StateMachineBehaviour
{
    Transform cerberoTransform;
    Enemy_Cerbero cerbero;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cerbero = animator.GetComponent<Enemy_Cerbero>();
        cerberoTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!cerbero.canMove) return;

        float xDist = Mathf.Abs(cerberoTransform.position.x - cerbero.player.transform.position.x);
        float yDist = Mathf.Abs(cerberoTransform.position.y - cerbero.player.transform.position.y);

        if (xDist > 20f || (xDist < 1f && yDist > 2f))
        {
            animator.SetBool("IsMove", false);
            return;
        }
        
        cerberoTransform.position = Vector2.MoveTowards(
            cerberoTransform.position,
            new Vector2(cerbero.player.transform.position.x, cerberoTransform.position.y),
            Time.deltaTime * cerbero.speed
        );
        
        animator.SetBool("IsMove", true);
        
        
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

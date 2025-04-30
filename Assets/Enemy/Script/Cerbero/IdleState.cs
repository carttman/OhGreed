using UnityEngine;

public class IdleState : StateMachineBehaviour
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
        if (!cerbero.canMove)
        {
            animator.SetBool("IsIdle", true);
            return;
        }
        
        float xDist = Mathf.Abs(cerberoTransform.position.x - cerbero.player.transform.position.x);
        float yDiff = cerbero.player.transform.position.y - cerberoTransform.position.y;
        
        if (xDist > 1f && xDist <= 20f && yDiff<= 6f)
        {
            animator.SetBool("IsMove", true); 
            animator.SetBool("IsIdle", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

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
        if (Vector2.Distance(cerberoTransform.position, cerbero.player.transform.position) <= 20)
            animator.SetBool("IsMove", true);
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

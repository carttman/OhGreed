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

        Vector2 current = cerberoTransform.position;
        Vector2 target = new Vector2(cerbero.player.transform.position.x, current.y);

        float xDist = Mathf.Abs(current.x - target.x);
        float yDist = Mathf.Abs(cerberoTransform.position.y - cerbero.player.transform.position.y);
        
        if (xDist > 20f || (xDist < 1f && yDist > 2f))
        {
            animator.SetBool("IsMove", false);
            animator.SetBool("IsIdle", true);
            return;
        }

        Vector2 newPos = Vector2.MoveTowards(current, target, Time.deltaTime * cerbero.speed);
        cerberoTransform.position = newPos;
        
        bool didMove = Mathf.Abs(newPos.x - current.x) > 0.001f;

        if (didMove)
        {
            animator.SetBool("IsMove", true);
            animator.SetBool("IsIdle", false);
        }
        else
        {
            animator.SetBool("IsMove", false);
            animator.SetBool("IsIdle", true);
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

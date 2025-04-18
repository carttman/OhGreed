using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    Transform cerberoTransform;
    Enemy_Cerbero cerbero;
    
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cerbero = animator.GetComponent<Enemy_Cerbero>();
        cerberoTransform = animator.GetComponent<Transform>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cerberoTransform.position = Vector2.MoveTowards(cerberoTransform.position, 
            new Vector2(cerbero.player.transform.position.x, cerberoTransform.position.y ), Time.deltaTime * cerbero.speed * 2);
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

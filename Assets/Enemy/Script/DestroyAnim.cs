using UnityEngine;

public class DestroyAnim : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Object.Destroy(animator.gameObject, stateInfo.length);
    }
}

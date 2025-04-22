using UnityEngine;

public class DestroyAnim : StateMachineBehaviour
{
    public float lifetime = 0.5f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Object.Destroy(animator.gameObject, lifetime);
    }
}

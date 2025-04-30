using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        animator.SetFloat("Walk", Mathf.Abs(playerMovement.horizontalMovement));
        animator.SetBool("IsJumping", !playerMovement.isGrounded || !playerMovement.isFloor);
    }
    

    // public void PlayHit()
    // {
    //     animator.SetTrigger("Hit");
    // }

    public void PlayDie()
    {
        animator.SetTrigger("Die");
    }
}
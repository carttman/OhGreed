using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private bool isFacingRight = true;

    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    
    [Header("Movement")]
    public float MoveSpeed = 5f;
    private float horizontalMovement;
    
    [Header("Jumping")]
    private float JumpPower = 10f;
    public int maxJumps = 2;
    private int jumpsRemaining;
    
    [Header("GroundCheck")] 
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    private bool isGrounded;
    
    [Header("Gravity")] 
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;
    
    [Header("WallCheck")] 
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask wallLayer;

    [Header("WallMovement")]
    public float wallSlideSpeed = 2;
    private bool isWallSliding;
    
    [Header("WallJump")]
    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpTime = 0.5f;
    private float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 10f);

    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        
        GroundCheck();
        Gravity();
        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * MoveSpeed, rb.linearVelocity.y);
            Flip();
        }
        
        //walk animation

        if (Mathf.Abs(horizontalMovement) > 0.1f)
        {
            animator.SetFloat("Walk", 1f);
            Debug.Log($"{horizontalMovement}");
        }
        else if (horizontalMovement < 0.1f)
        {
            animator.SetFloat("Walk", 0f);
        }
        //jump animation
        animator.SetBool("IsJumping", !isGrounded);
        
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpPower);
                
                jumpsRemaining--;
            }
            else if (context.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                
                jumpsRemaining--;
            }
        }
        
        //Wall jump
        if (context.performed && wallJumpTimer > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0;
            //force flip
            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }
            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int randomIndex = Random.Range(1, 4);
            
            animator.SetTrigger($"Attack{randomIndex}");
            
        }
    }

    public void DoAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        Debug.Log("들어가니?");
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log($"적을 공격했어!"+ enemy.name);
            //enemy.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    
    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private bool wallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }
    
    private void Gravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
    
    private void WallSlide()
    {
        if (!isGrounded & wallCheck() & horizontalMovement != 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;
            
            CancelInvoke(nameof(CancelWallJump));
        }
        else if (wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    
    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.white;
    //     Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    //     
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    // }
    

}
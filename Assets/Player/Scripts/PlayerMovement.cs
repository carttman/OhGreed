using System.Numerics;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    private PlayerHealth playerHealth;
    
    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;
    
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float horizontalMovement;
    
    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    private int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    public bool IsGrounded;

    [Header("WallCheck")]
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask wallLayer;
    
    [Header("WallMovement")]
    public float wallSlideSpeed = 2f;
    private bool isWallSliding;
    
    [Header("WallJump")]
    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpTime = 0.5f;
    private float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(8f, 12f);

    [Header("Dash")]
    public GameObject ghostPrefab;
    
    public float dashForce = 15f;
    public float dashTime = 0.2f;
    public float dashCooldownTime = 1f;
    public float ghostSpawnInterval = 0.05f;
    
    private bool isFacingRight = true;
    private bool isDashingCooldown = false;
    private int dashCount = 0;
    private float originalGravityScale;

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float ghostTimer = 0f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        originalGravityScale = rb.gravityScale;

        isFacingRight = true;
        Vector3 scale = transform.localScale;
        scale.x = isFacingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    void Update()
    {
        FlipByMouse();
        
        GroundCheck();
        Gravity();
        WallSlide();
        WallJump();
        
        ApplyMovement();
        
        HandleGhostTrail();
    }

    private void ApplyMovement()
    {
        if (isWallJumping || dashCount > 0) return;
        
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //벽 점프
        if (context.performed && wallJumpTimer > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0;

            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.5f);
            return;
        }
        
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                jumpsRemaining--;
            }
            else if (context.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                jumpsRemaining--;
            }
        }
    }

    private void GroundCheck()
    {
        IsGrounded = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
        if (IsGrounded)
        {
            jumpsRemaining = maxJumps;
        }
    }

    private bool WallCheck()
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
        if (!IsGrounded && WallCheck() && horizontalMovement != 0)
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
    
    private void FlipByMouse()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        bool mouseOnRight = mouseWorldPos.x > transform.position.x;

        if (mouseOnRight != isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if (playerHealth != null && playerHealth.isDead)
            return;
        
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started && dashCount ==0 && !isDashingCooldown)
        {
            dashCount++;
            isDashingCooldown = true;
            isDashing = true;

            dashTimer = 0f;
            ghostTimer = 0f;

            originalGravityScale = rb.gravityScale;
            rb.gravityScale = 0f;

            rb.linearVelocity = Vector2.zero;
            Vector2 dir = GetMouseDirection();
            rb.AddForce(dir.normalized * dashForce, ForceMode2D.Impulse);

            Invoke(nameof(EndDash), dashTime);
            Invoke(nameof(ResetDashCooldown), dashCooldownTime);
        }
    }

    private void HandleGhostTrail()
    {
        if (!isDashing) return;

        dashTimer += Time.deltaTime;
        ghostTimer += Time.deltaTime;
        
        if (ghostTimer >= ghostSpawnInterval)
        {
            SpawnGhost();
            ghostTimer = 0f;
        }

        if (dashTimer >= dashTime)
        {
            EndDash();
        }
    }

    private void SpawnGhost()
    {
        if (ghostPrefab != null)
        {
            var ghost = Instantiate(ghostPrefab, transform.position, Quaternion.identity);
        }
    }

    private void EndDash()
    {
        isDashing = false;
        rb.gravityScale = originalGravityScale;
        dashCount = 0;
    }

    private void ResetDashCooldown()
    {
        isDashingCooldown = false;
    }

    private Vector2 GetMouseDirection()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        return (mouseWorldPos - transform.position);
    }
}

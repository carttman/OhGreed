using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement02 : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Movement")]
    public float MoveSpeed = 5f;
    private float horizontalMovement;
    
    [Header("Jumping")]
    private float JumpPower = 10f;

    [Header("GroundCheck")] 
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * MoveSpeed, rb.linearVelocity.y);
    }

    // public void Move(InputAction.CallbackContext context)
    // {
    //     horizontalMovement = context.ReadValue<Vector2>().x;
    // }
    //
    // public void Jump(InputAction.CallbackContext context)
    // {
    //     if (isGrounded())
    //     {
    //         if (context.performed)
    //         {
    //             rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpPower);
    //         }
    //         else if (context.canceled)
    //         {
    //             rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
    //         }
    //     }
    //     
    // }

    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            return true;
        }

        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}


using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    
    [Header("Movement")]
    public float MoveSpeed = 5f;
    private float horizontalMovement;
    
    [Header("Jump")]
    private float JumpPower = 10f;

   
    void Start()
    {
        
    }
    
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * MoveSpeed, rb.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Hold down jump button = full height
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpPower);
        }
        
        else if (context.canceled)
        {
            //Hold down jump button = half the height
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }
}

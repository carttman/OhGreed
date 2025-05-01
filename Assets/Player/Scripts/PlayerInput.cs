using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCombat))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMovement.Move(context);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        playerMovement.Jump(context);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        playerCombat.Attack(context);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        playerMovement.Dash(context);
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        playerMovement.Drop(context);
    }
}


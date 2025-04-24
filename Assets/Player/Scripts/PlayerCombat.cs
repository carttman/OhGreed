using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private PlayerAnimation playerAnimation;

    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (ItemManager.Instance.WeaponAttackable != null)
            {
                ItemManager.Instance.WeaponAttackable.Attack();
            }

            // int randomIndex = Random.Range(1, 4);
            // playerAnimation.PlayAttack(randomIndex);
        }
    }

    public void DoAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("적을 공격했어!" + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    private PlayerAnimation playerAnimation;

    private void Awake()
    {
        currentHealth = maxHealth;
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        playerAnimation.PlayHit();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player 사망!");
        playerAnimation.PlayDie();
        Destroy(gameObject, 1.5f);
    }
}


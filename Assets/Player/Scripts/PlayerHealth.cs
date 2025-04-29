using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 97;
    private int currentHealth;
    public bool isDead = false;
    
    private PlayerAnimation playerAnimation;

    public PlayerHpBarController healthBar;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealth(currentHealth);
        }
        else
        {
            Debug.LogWarning("HealthBar가 연결되지 않았어!");
        }
    }
    

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.SetCurrentHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Player 사망!");
        playerAnimation.PlayDie();
        
        GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled = false;
        
    }
    
}


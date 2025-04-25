using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 97;
    private int currentHealth;
    
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
            Debug.Log("hp바 업데이트됨");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player 사망!");
        playerAnimation.PlayDie();
        //GetComponent<PlayerInput>().enabled = false;
        //죽었을 때 이동 마우스 공격 안되게///
    }
}


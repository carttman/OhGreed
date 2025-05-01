using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public bool gameEnd = false;
    public bool isInvincible = false;
    
    //BossRoomManager 연결하기
    [SerializeField] private BossRoomManager bossRoomManager;
    private float currentHealth;
    public float maxHealth = 5;
    
    public Slider healthSlider;
    public GameObject healthBarUI;
    private SpriteRenderer spriteRenderer;

    public bool isBoss = false;
    private Boss_Attack bossAttack;
    public GameObject dieAnim;
    
    private bool bossDead = false;
    void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (isBoss)
        {
            bossAttack = GetComponentInParent<Boss_Attack>();
        }

        if (ItemManager.Instance.Player.GetComponent<PlayerHealth>().EnemyHealth == null)
        {
            ItemManager.Instance.Player.GetComponent<PlayerHealth>().EnemyHealth = this;
        }
    }

    public void TakeDamage(float damage)
    {
        if(isInvincible) return;
        
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        
        
        if (currentHealth <= 0)
        {
            Die();
        }

        if (currentHealth > 0)
        {
            DamageTextManager.Instance.SpawnDamageText(transform.position, damage);
            StartCoroutine(DamageRed());
        }
      
    }

    private void Die()
    {
        Vector3 offset = new Vector3(0, 1.5f, 0);

        if (isBoss)
        {
            if (!bossDead)
            {
                if (gameEnd) return;
                gameEnd = true;
                
                bossDead = true;
                bossAttack?.Die();
                Destroy(healthBarUI);
                Instantiate(dieAnim, transform.position + offset, Quaternion.identity);
            }
        }
        else
        {
            Instantiate(dieAnim, transform.position + offset, Quaternion.identity);
            Destroy(gameObject);
            Destroy(healthBarUI);
            
            if (bossRoomManager != null)
            {
                bossRoomManager.EnemyDefeated(); // ⭐ 죽을 때 BossRoomManager한테 알려줌
            }

        }
        
    }

    IEnumerator DamageRed()
    {
       
        spriteRenderer.color = new Color(1f, 0f, 0f, 1f); 
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}

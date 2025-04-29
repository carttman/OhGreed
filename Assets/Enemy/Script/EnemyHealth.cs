using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
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
    
    void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (isBoss)
        {
            bossAttack = GetComponentInParent<Boss_Attack>();
        }
        


        // if (bossRoomManager != null)
        // {
        //     bossRoomManager.RegisterEnemy(); // 태어날 때 등록
        // }
        // else
        // {
        //     Debug.LogWarning("BossRoomManager를 찾을 수 없습니다");
        // }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        
        DamageTextManager.Instance.SpawnDamageText(transform.position, damage);
        
        if (currentHealth <= 0)
        {
            Die();
        }

        StartCoroutine(DamageRed());
    }

    private void Die()
    {
        Vector3 offset = new Vector3(0, 1.5f, 0);

        if (isBoss)
        {
            bossAttack?.Die();
            Destroy(healthBarUI);
            Instantiate(dieAnim, transform.position + offset, Quaternion.identity);
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

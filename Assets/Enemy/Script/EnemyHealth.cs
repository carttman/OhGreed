using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 80;
    
    public Slider healthSlider;
    public GameObject healthBarUI;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        
        if (currentHealth <= 0)
        {
            Die();
        }

        StartCoroutine(DamageRed());
    }
    
    private void Die()
    {
        Destroy(gameObject);
        Destroy(healthBarUI);
    }
    
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }*/

    IEnumerator DamageRed()
    {
        spriteRenderer.color = new Color(1f, 0f, 0f, 1f); 
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}

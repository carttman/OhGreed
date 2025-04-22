using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 5;
    
    public Slider healthSlider;
    public GameObject healthBarUI;
    private SpriteRenderer spriteRenderer;

    public GameObject dieAnim;
    
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
        Vector3 offset = new Vector3(0, 0.5f, 0);
        
        Instantiate(dieAnim, transform.position + offset , Quaternion.identity);
        Destroy(gameObject);
        Destroy(healthBarUI);
        
    }

    IEnumerator DamageRed()
    {
        spriteRenderer.color = new Color(1f, 0f, 0f, 1f); 
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}

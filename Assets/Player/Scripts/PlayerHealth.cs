using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 97;
    private int currentHealth;
    public bool isDead = false;
    
    public GameObject gameOverUI;
    public GameObject boss;
    
    private PlayerAnimation playerAnimation;

    public PlayerHpBarController healthBar;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        currentHealth = maxHealth;
        
        gameOverUI.SetActive(false);

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
        playerAnimation.PlayDie();
        
        Boss_Attack bossAttack = boss.GetComponent<Boss_Attack>();
        bossAttack.StopAttack();
        
        GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled = false;
        StartCoroutine(GameOver());
        
    }
    
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        
        gameOverUI.SetActive(true);
        
        CanvasGroup canvasGroup = gameOverUI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f; 
        
        float duration = 1.5f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime; 
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnClickMainMenu()
    {
        Transform player = GameManager.Instance.transform.Find("Player");
        Destroy(player.gameObject);
        SceneManager.LoadScene("LYS_Start"); 
    }
}



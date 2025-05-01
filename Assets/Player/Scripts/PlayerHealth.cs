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
    
    public AudioSource sfxSource;
    public AudioClip deathClip;
    public AudioClip hitClip;

    public Boss_Attack bossAttack;

    public EnemyHealth EnemyHealth;
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
        sfxSource.PlayOneShot(hitClip);
        
        if (healthBar != null)
        {
            healthBar.SetCurrentHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Debug.Log("111111111111111111111111111111111");
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        Debug.Log("22222222222222222222222222222222222222222222222");

        
        if(EnemyHealth.gameEnd) return;
        
        Debug.Log("333333333333333333333333333333333333333333333333");
        
        EnemyHealth.gameEnd = true;

        isDead = true;
        playerAnimation.PlayDie();
        
        
        //Boss_Attack bossAttack = boss.GetComponent<Boss_Attack>();
        if (bossAttack)
        {
            bossAttack.StopAttack();
        }
        
        GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled = false;
        StartCoroutine(GameOver());

        BossBGMStop.Instance.Stop();
    }
    
    private IEnumerator GameOver()
    {
        var overUI = Instantiate(gameOverUI, Camera.main.transform.position + new Vector3(0, 0, 0.01f), Quaternion.identity);
        overUI.transform.SetParent(Camera.main.transform);
        
        yield return new WaitForSeconds(2f);
        overUI.SetActive(true);
        
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(deathClip);

        
        CanvasGroup canvasGroup = overUI.GetComponent<CanvasGroup>();
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
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene("Town"); 
    }
}



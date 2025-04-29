using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeath : MonoBehaviour
{
    public CanvasGroup whiteScreen;
    public GameObject deathEffect;
    public Sprite deadHead;
    public GameObject player;
    
    public Animator headAnim;
    public Animator leftHandAnim;
    public Animator rightHandAnim;
    public GameObject backParticle;
    public GameObject clearUI;
    
    public IEnumerator Death()
    {
        // 화면 하얗게, 슬로우
        whiteScreen.alpha = 1f;
        Time.timeScale = 0.3f;

        headAnim.enabled = false;
        leftHandAnim.enabled = false;
        rightHandAnim.enabled = false;
        Destroy(backParticle);

        float timer = 0f;
        float totalTime = 4f;

        while (timer < totalTime)
        {
            timer += Time.unscaledDeltaTime;
            whiteScreen.alpha = Mathf.Lerp(1f, 0f, timer / totalTime);
            yield return null;
        }

        Time.timeScale = 1f;
        whiteScreen.alpha = 0f;


        // 파티클
        int num = 50;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < num; j++)
            {
                Vector2 randomPos = new Vector2(Random.Range(-7, 7), Random.Range(-1, 10));
                Instantiate(deathEffect, randomPos, Quaternion.identity);
                
                yield return new WaitForSeconds(Random.Range(0.02f, 0.08f));
            }
        }
        yield return new WaitForSeconds(0.1f);
        
        
        // 보스 머리 애니메이션
        SpriteRenderer headRenderer = headAnim.GetComponent<SpriteRenderer>();
        headRenderer.sprite = deadHead;
        headAnim.transform.position += new Vector3(-0.5f, 1.5f, 0f);
        
        Rigidbody2D headRb = headAnim.gameObject.GetComponent<Rigidbody2D>();
        headRb.bodyType = RigidbodyType2D.Dynamic;
        
        CircleCollider2D headCollider = headAnim.GetComponent<CircleCollider2D>();
        headCollider.offset = new Vector2(0f, 0f);
        yield return new WaitForSeconds(2f);
        
        //종료 화면
        CanvasGroup canvasGroup = clearUI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        
        clearUI.SetActive(true);
        
        float duration = 1.5f;
        float timer2 = 0f;

        while (timer2 < duration)
        {
            timer2 += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer2 / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    
    public void OnClickMainMenu()
    {
        Destroy(player);
        SceneManager.LoadScene("LYS_Start");
    }
}

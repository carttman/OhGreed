using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class BossIntro : MonoBehaviour
{
    public GameObject head;
    public GameObject particle;
    public GameObject handL;
    public GameObject handR;
    
    public CanvasGroup canvasGroup;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip laughClip;

    public IEnumerator Intro()
    {
        sfxSource.PlayOneShot(laughClip);
        yield return StartCoroutine(FadeInObject(head, 0.3f));
        particle.SetActive(true);
        
        bgmSource.Play();
        
        yield return StartCoroutine(FadeInObject(handL, 0.3f));
        yield return StartCoroutine(FadeInObject(handR, 0.3f));
        
        canvasGroup.alpha = 0f;
        
        float timer = 0f;
        float totalTime = 1f;

        while (timer < totalTime)
        {
            timer += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / totalTime);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        
        timer = 0f;
        yield return new WaitForSeconds(2.5f);
        
        while (timer < totalTime)
        {
            timer += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / totalTime);
            yield return null;
        }
        
        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
    }
    
    private IEnumerator FadeInObject(GameObject obj, float totalTime)
    {
        obj.SetActive(true);
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();

        Color color = sr.color;
        color.a = 0f;
        sr.color = color;

        float timer = 0f;

        while (timer < totalTime)
        {
            timer += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / totalTime);
            sr.color = color;
            yield return null;
        }

        color.a = 1f;
        sr.color = color;
    }
}

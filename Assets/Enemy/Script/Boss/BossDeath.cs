using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossDeath : MonoBehaviour
{
    public CanvasGroup whiteScreen;
    public Animator headAnimator;
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;
    public GameObject behindParticle;

    void Start()
    {
        StartCoroutine(Death());
    }
    
    public IEnumerator Death()
    {
        whiteScreen.alpha = 1f;

        Time.timeScale = 0.3f;

        float timer = 0f;
        float totalTime = 3f;
        
        while (timer < totalTime)
        {
            timer += Time.unscaledDeltaTime;
            whiteScreen.alpha = Mathf.Lerp(1f, 0f,timer / totalTime);
            yield return null;
        }
        Time.timeScale = 1f;
        whiteScreen.alpha = 0f;
    }
}

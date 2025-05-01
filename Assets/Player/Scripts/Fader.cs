using System;
using System.Collections;
using DG.Tweening;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public float duration = 0.5f;
    private CanvasGroup canvasGroup;

    public static Fader Instance;

    private void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(gameObject);
    }

    public static void Play()
    {
        Instance.StopAllCoroutines();
        Instance.StartCoroutine(Process());
    }

    private static IEnumerator Process()
    {
        yield return Instance.canvasGroup.DOFade(1f, Instance.duration).WaitForCompletion();
        yield return new WaitForSeconds(Instance.duration);
        yield return Instance.canvasGroup.DOFade(0f, Instance.duration).WaitForCompletion();
    }

}

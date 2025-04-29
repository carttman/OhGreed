using UnityEngine;
using System.Collections;
using TMPro;

public class DamageText : MonoBehaviour
{
    public TextMeshPro textMesh;

    public void Setup(float damage)
    {
        textMesh.text = Mathf.CeilToInt(damage).ToString();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Vector3 moveOffset = new Vector3(0.5f, 2f, 0);
        float duration = 0.8f;
        float timer = 0f;

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + moveOffset;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, timer / duration);
            yield return null;
        }

        Destroy(gameObject);
    }
}

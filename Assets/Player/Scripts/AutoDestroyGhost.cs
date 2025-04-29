using UnityEngine;

public class AutoDestroyGhost : MonoBehaviour
{
    public float lifetime = 0.3f;
    private SpriteRenderer sr;
    private float timer = 0f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        float alpha = Mathf.Lerp(0.5f, 0f, timer / lifetime);
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;

        if (timer >= lifetime)
            Destroy(gameObject);
    }
}

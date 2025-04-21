using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;
    public Transform target;
    public Vector3 offset;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    void FixedUpdate()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);
        rect.position = screenPos;
    }
}

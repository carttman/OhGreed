using UnityEngine;

public class Enemy_Cerbero : MonoBehaviour
{
    public Transform player;
    public Transform cerbero;
    public float speed;
    private SpriteRenderer spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x < transform.position.x)
            spriteRenderer.flipX = true;  
        else
            spriteRenderer.flipX = false; 
    }
}

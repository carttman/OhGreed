using System;
using System.Collections;
using UnityEngine;

public class Enemy_Cerbero : MonoBehaviour
{
    public Transform player;
    public Transform cerbero;
    public Animator animator;
    public float speed;
    private SpriteRenderer spriteRenderer;
    public bool canMove = true;
    public bool inRange = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 3f;
        StartCoroutine("WaitAttack");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x < transform.position.x)
            spriteRenderer.flipX = true;  
        else
            spriteRenderer.flipX = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }


    IEnumerator WaitAttack()
    {
        while (true)
        {
            yield return new WaitUntil(() => inRange);
            
            canMove = false;
            animator.SetBool("IsMove", false);
            animator.SetBool("IsIdle", true);

            yield return new WaitForSeconds(1.5f);
            
            animator.SetTrigger("Attack");
            canMove = true;
        }
    }
}

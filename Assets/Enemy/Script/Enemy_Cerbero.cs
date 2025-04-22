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
    
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask playerLayer;

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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Cer_Attack"))
            return;
        
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
    
    public void DealDamageToPlayer()
    {
        Debug.Log("cerbero 공격 이벤트");
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (hit != null && hit.CompareTag("Player"))
        {
            hit.GetComponent<PlayerHealth>()?.TakeDamage(1);
            Debug.Log("cerbero 데미지");
        }
    }
}

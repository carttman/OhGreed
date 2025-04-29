using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class Boss_Sword : MonoBehaviour
{
    public GameObject swordPrefab;
    public GameObject startAnim;
    public GameObject HitAnim;
    public GameObject DieAnim;
    public Sprite attackSprite;
    
    public Transform player;
    public int swordCount = 6;
    public float spawnSpacing = 1.5f;
    public float spawnHeight = 5f;
    public float prepareTime = 2f;
    public int speed = 50;
    
    private Sprite originalSprite;
    private List<bool> isPreparing = new List<bool>();
    private List<GameObject> swords = new List<GameObject>();

    void Start()
    {
        SpriteRenderer spriteRenderer = swordPrefab.GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {
        for(int i = 0; i < isPreparing.Count; i++)
        {
            if(isPreparing[i])
            {
                LookAtPlayer(swords[i].transform);
            }
        }
    }

    public IEnumerator Sword()
    {
        
        swords.Clear();
        isPreparing.Clear();
        
        Vector3 startPos = new Vector3(-((swordCount - 1) * spawnSpacing) / 2f, spawnHeight, 0f);

        for (int i = 0; i < swordCount; i++)
        {
            Vector3 spawnPos = startPos + new Vector3(i * spawnSpacing, 0f, 0f);
            GameObject sword = Instantiate(swordPrefab, spawnPos, Quaternion.identity);
            
            GameObject Start = Instantiate(startAnim, spawnPos + new Vector3(0,-0.7f,0), Quaternion.identity);
            Destroy(Start,0.5f);
            
            sword.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            swords.Add(sword);
            isPreparing.Add(true);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(prepareTime);
        
        
                    
        for (int i = 0; i < swords.Count; i++)
        {
            GameObject sword = swords[i];
            Rigidbody2D rb = sword.GetComponent<Rigidbody2D>();
            
            Animator swordAnim = sword.GetComponentInChildren<Animator>();
            swordAnim.SetTrigger("Stop");
            
            SpriteRenderer spriteRenderer = sword.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = attackSprite;
            
            Vector2 dir = (player.position - sword.transform.position).normalized;
            rb.linearVelocity = dir * speed;
            
            isPreparing[i] = false;
            
            StartCoroutine(StopSword(rb));
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void LookAtPlayer(Transform sword)
    {
        Vector2 dir = player.position - sword.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        sword.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }

    private IEnumerator StopSword(Rigidbody2D rb)
    {
        bool effect = false;
        
        while (true)
        {
            if (rb.transform.position.y <= -2f)
            {
                if (!effect)
                {
                    GameObject Hit = Instantiate(HitAnim, new Vector3(rb.transform.position.x, -3.1f, 0), Quaternion.identity);
                    Destroy(Hit, 0.5f);

                    SpriteRenderer spriteRenderer = rb.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = originalSprite;
                    
                    rb.gravityScale = 0f;
                    rb.linearVelocity = Vector2.zero;
                    
                    effect = true;
                    
                   yield return new WaitForSeconds(3f);
                   Destroy(rb.gameObject);
                   
                   GameObject Die = Instantiate(DieAnim, rb.transform.position, Quaternion.identity);
                   Destroy(Die, 0.3f);
                }
            }
            yield return null;
        }
    } 
} 
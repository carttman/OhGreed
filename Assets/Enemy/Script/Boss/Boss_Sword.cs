using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class Boss_Sword : MonoBehaviour
{
    public GameObject swordPrefab;
    public Transform player;
    public int swordCount = 6;
    public float spawnSpacing = 1.5f;
    public float spawnHeight = 5f;
    public float prepareTime = 2f;
    
    private List<GameObject> swords = new List<GameObject>();
    
    void Start()
    {
        StartCoroutine(SwordAttack());
    }

    public IEnumerator SwordAttack()
    {
        Vector3 startPos = new Vector3(-((swordCount - 1) * spawnSpacing) / 2f, spawnHeight, 0f);

        for (int i = 0; i < swordCount; i++)
        {
            Vector3 spawnPos = startPos + new Vector3(i * spawnSpacing, 0f, 0f);
            GameObject sword = Instantiate(swordPrefab, spawnPos, Quaternion.identity);

            sword.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            LookAtPlayer(sword.transform);
            
            swords.Add(sword);

            yield return new WaitForSeconds(0.08f);
        }
        
        float timer = 0f;
        while (timer < prepareTime)
        {
            timer += Time.deltaTime;

            foreach (GameObject sword in swords)
            {
                LookAtPlayer(sword.transform);
            }
            yield return null;
        }
        
    }
    
    private void LookAtPlayer(Transform sword)
    {
        Vector2 dir = player.position - sword.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        sword.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }
    
}

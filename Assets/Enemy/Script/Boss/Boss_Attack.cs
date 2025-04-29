using System.Collections;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{
    public Boss_Laser laser;
    public Boss_Sword sword;
    public Boss_Circle circle;
    public BossDeath death;

    public GameObject bossprefab;

    private bool isAlive = true;
    private int pattern;
    
    void Start()
    {
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(5f); 
            
            switch (pattern)
            {
                case 0:
                    yield return StartCoroutine(laser.Laser());
                    break;
                case 1:
                    yield return StartCoroutine(sword.Sword());
                    break;
                case 2:
                    yield return StartCoroutine(circle.Circle());
                    break;
            }

            pattern++;
            if (pattern > 2)
                pattern = 0; 
        }
    }
    
    public void Die()
    {
        isAlive = false;
        StopAllCoroutines();
        StartCoroutine(death.Death());
    }
    
    public void StopAttack()
    {
        isAlive = false;
        StopAllCoroutines();
    }
}

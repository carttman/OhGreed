using System.Collections;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{
    public BossIntro intro;
    public Boss_Laser laser;
    public Boss_Sword sword;
    public Boss_Circle circle;
    public BossDeath death;

    private bool isAlive = true;
    private int pattern;

    void Start()
    {
        StartCoroutine(StartAfterIntro());
    }
    
    private IEnumerator StartAfterIntro()
    {
        yield return StartCoroutine(intro.Intro());
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(3f); 
            
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

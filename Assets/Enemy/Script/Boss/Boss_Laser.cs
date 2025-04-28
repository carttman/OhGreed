using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Boss_Laser : MonoBehaviour
{
    public Animator handAnimL;
    public Animator handAnimR;
    public GameObject laser;
    public Transform laserL;
    public Transform laserR;
    public Transform player;
    
    public int speed = 10;
    public float delay = 1f;
    
    private bool isAttackingL = false;
    private bool isAttackingR = false;
    public void Start()
    {
        StartCoroutine(Laser());
    }

    void Update()
    {
        Transform handL = handAnimL.transform;
        Transform handR = handAnimR.transform;

        if (!isAttackingL)
        {
            MoveHand(handL, handR);
        }

        else if (!isAttackingR)
        {
            MoveHand(handR, handL);
        }
    }

    void MoveHand(Transform myHand, Transform otherHand)
    {
        float playerDist = Mathf.Abs(myHand.position.y - player.position.y);
        float playerOtherDist = Mathf.Abs(player.position.y - otherHand.position.y);

        if (playerDist <= 1.5f)
        {
            myHand.position = myHand.position;
        }
        
        else if (playerDist > 3 && playerDist < playerOtherDist) 
        {
            myHand.position = Vector2.MoveTowards(myHand.position,
                new Vector2(myHand.position.x, player.position.y), speed * Time.deltaTime);
        }
        
        else if (playerDist > 3 && playerDist > playerOtherDist)
        {
            myHand.position = Vector2.MoveTowards(myHand.position, 
                new Vector2(myHand.position.x, otherHand.position.y + 3) , speed * Time.deltaTime);
        }
        
        else if (playerDist < 3 && playerDist < playerOtherDist)
        {
            myHand.position = Vector2.MoveTowards(myHand.position, 
                new Vector2(myHand.position.x, player.position.y), speed * Time.deltaTime);
        }
    }

    private IEnumerator Laser()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            isAttackingL = true;
            handAnimL.SetTrigger("Attack");

            yield return new WaitForSeconds(0.8f);
            Instantiate(laser, laserL.position, Quaternion.identity);
            isAttackingL = false;

            yield return new WaitForSeconds(delay);

            isAttackingR = true;
            handAnimR.SetTrigger("Attack");

            yield return new WaitForSeconds(0.8f);
            GameObject rightLaser = Instantiate(laser, laserR.position, Quaternion.identity);
            SpriteRenderer sr = rightLaser.GetComponent<SpriteRenderer>();
            sr.flipX = true;
            isAttackingR = false;
        }
    }
}


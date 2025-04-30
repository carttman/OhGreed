using UnityEngine;
using System.Collections;

public class Boss_Circle : MonoBehaviour
{
    public GameObject circle;
    public Transform spawnPoint;
    public Animator head;

    public int speed = 5;
    public int lifetime = 5;
    public int num = 10;
    public float delay = 0.5f;

    public AudioSource sfxSource;
    public AudioClip circleClip;
    
    private float rotateAngle = 0f;

    public IEnumerator Circle()
    {
        head.SetTrigger("Attack");
        
        yield return new WaitForSeconds(0.7f);
        
        
        for (int i = 0; i < num; i++)
        {
            sfxSource.PlayOneShot(circleClip);
            
            for (int j = 0; j < 4; j++)
            {
                // 라디안 단위 만들기 
                float rad = (rotateAngle + (90 * j)) * Mathf.Deg2Rad;
                Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

                GameObject cir = Instantiate(circle, spawnPoint.position, Quaternion.identity);

                Rigidbody2D rb = cir.GetComponent<Rigidbody2D>();
                rb.linearVelocity = dir.normalized * speed;

                Destroy(cir, lifetime);

                rotateAngle += 1.5f;

                yield return new WaitForSeconds(delay/4);
            }

        }
    }
}
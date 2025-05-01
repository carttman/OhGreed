using System.Collections;
using UnityEngine;

public class FlameSnake : MonoBehaviour
{
    [SerializeField]
    private GameObject FireBullet;
    private Animator animator;
    
    [SerializeField]
    private Transform BulletSpawnPoint;

    [SerializeField] 
    private GameObject AttackBeginVFX;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartFireAndDestroy());
    }

    IEnumerator StartFireAndDestroy()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(2);
            animator.SetTrigger("Fire");
        }
        
        yield return new WaitForSeconds(3);
        
        Destroy(gameObject);
    }

    public void OnBeginFireBullet()
    {
        var bulletBeginVFX = Instantiate(AttackBeginVFX, BulletSpawnPoint.position, transform.rotation);
        Destroy(bulletBeginVFX, 1f);
    }
    
    public void OnSpawnFireBullet()
    {
        var bullet = Instantiate(FireBullet, BulletSpawnPoint.position, transform.rotation);
        bullet.GetComponent<FireBullet>().currScale = transform.localScale.x;
    }
    
    public void PlaySound(AudioClip clip)
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }
}

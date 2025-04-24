using System.Collections;
using UnityEngine;

public class KatanaCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log($"{collision.gameObject.name}이(가) Enemy 레이어에서 감지됨!");

            var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            StartCoroutine(DamageCoroutine(enemyHealth));
        }
        
        IEnumerator DamageCoroutine(EnemyHealth enemyHealth)
        {
            for (int i = 0; i < 5; i++)
            {
                if (enemyHealth)
                {
                    
                    enemyHealth.TakeDamage(1);
                    yield return new WaitForSeconds(0.1f);
                
                }
            }
        }
    }
}


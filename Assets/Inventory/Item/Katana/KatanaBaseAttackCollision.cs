using System.Collections;
using UnityEngine;

public class KatanaBaseAttackCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log($"{collision.gameObject.name}이(가) Enemy 레이어에서 감지됨!");

            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            
        }
    }
}

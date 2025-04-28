using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int damage = 5;
    private bool ground = false;

    private void Update()
    {
        if (transform.position.y <= -2f)
        {
            ground = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ground)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            }
        }
    }
}

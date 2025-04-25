using Unity.Cinemachine;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }
    public void Destroy()
    {
        {
            Destroy(gameObject);
        }
    }
}
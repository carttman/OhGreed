using UnityEngine;

public class Boss_Circle : MonoBehaviour
{
    public GameObject circle;
    public Transform spawnPoint;
    public int speed = 5;
    public int lifetime = 5;
    public Vector2 direction = Vector2.up;

    void Start()
    {
        CircleAttack();
    }

    public void CircleAttack()
    {
        GameObject cir = Instantiate(circle, spawnPoint.position, Quaternion.identity);
        
        Rigidbody2D rb = cir.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction.normalized * speed;
        
        Destroy(cir, lifetime);
    }
}

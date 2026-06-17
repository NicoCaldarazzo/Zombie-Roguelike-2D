using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 bulletStartPos;
    private float damage;
    private float speed;
    private float range;
    private Rigidbody2D rb;
    private Vector3 direction;

    public void Initialize(float damage, float speed, float range, Vector3 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        bulletStartPos = transform.position;
        this.direction = direction;
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        rb.linearVelocity = direction * speed;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(bulletStartPos, transform.position) > range)
        {
            Destroy(gameObject); // Destroy the bullet if it exceeds its range
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            ZombieTracking zombie = collision.GetComponent<ZombieTracking>();
            if (zombie != null)
            {
                
                zombie.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject); 
        }
    }
}

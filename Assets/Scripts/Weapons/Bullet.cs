using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 bulletStartPos;
    private float damage;
    private float speed;
    private float range;
    private float bulletSize;
    private Rigidbody2D rb;
    private Vector2 direction;
    private CircleCollider2D col;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }
    public void Initialize(float damage, float speed, float range, Vector2 direction, float bulletSize)
    {
        bulletStartPos = transform.position;
        this.direction = direction;
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        
        //Bullet size modifier
        Vector3 originalScale = transform.localScale;
        transform.localScale = new Vector3 (originalScale.x * bulletSize, originalScale.y * bulletSize, 1f);
        rb.linearVelocity = direction * speed;
        col.radius *= bulletSize;
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

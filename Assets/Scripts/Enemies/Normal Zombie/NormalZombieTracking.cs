using System.Collections;
using UnityEngine;

public class ZombieTracking : MonoBehaviour
{
    [SerializeField] private float zombieSpeed = 1f;
    [SerializeField] private float zombiehealth = 1f;
    [SerializeField] private int pointValue = 1;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackDamage = 0.5f;
    private bool isAttacking = false;
    private Rigidbody2D rb;
    private Transform player;
    private PlayerHealth playerHealth;
    public Vector2 Direction{ get; private set;}

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void FixedUpdate()
    {
        if (player != null && !isAttacking)
        {
            Direction = (player.position - transform.position).normalized;
            
            rb.linearVelocity = Direction * zombieSpeed;
        }
    }

    public void TakeDamage(float damage)
    {
        zombiehealth -= damage;
        if (zombiehealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameEvents.ZombieKilled(pointValue);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAttacking)
        {
            playerHealth = collision.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerHealth != null && !isAttacking)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = false;
            StopAllCoroutines();
            playerHealth = null;
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        while (isAttacking)
        {
            rb.linearVelocity = Vector2.zero; // Stop moving while attacking
            if (playerHealth != null) playerHealth.TakeDamage(attackDamage);
            Debug.Log("Zombie attacked player for " + attackDamage + " damage.");
            yield return new WaitForSeconds(attackCooldown);
            isAttacking = false; // Allow movement again after attack cooldown
        }
    }
}

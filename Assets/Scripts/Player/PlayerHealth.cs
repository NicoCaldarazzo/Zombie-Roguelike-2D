using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float baseRegenRate = 0f;
    private float finalMaxHealth;
    private float finalRegenRate;
    private float currentHealth;
    private UpgradeStats upgradeStats;

    private bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        upgradeStats = GetComponent<UpgradeStats>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        finalMaxHealth = maxHealth;
        finalRegenRate = baseRegenRate;
    }
    void OnEnable()
    {
        GameEvents.OnUpgradeApplied += RecalculateStats;
    }
    void OnDisable()
    {
        GameEvents.OnUpgradeApplied -= RecalculateStats;
    }
    void Update()
    {
        if (!isDead && currentHealth < finalMaxHealth)
        {
            currentHealth += finalMaxHealth * (finalRegenRate / 100f ) * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, finalMaxHealth);
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        Debug.Log("Player took " + damage + " damage, current health: " + currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    private void RecalculateStats()
    {
        float oldMaxHealth = finalMaxHealth;
        finalMaxHealth = maxHealth + upgradeStats.GetModifier(UpgradeType.MaxHealth);
        float healthIncrease = finalMaxHealth - oldMaxHealth;
        currentHealth += healthIncrease;
        currentHealth = Mathf.Clamp(currentHealth, 0 , finalMaxHealth);
        finalRegenRate = baseRegenRate + upgradeStats.GetModifier(UpgradeType.HealthRegen);
        healthSlider.maxValue = finalMaxHealth;
        healthSlider.value = currentHealth;
    }

    private void Die()
    {
        GameEvents.PlayerDeath();
        Debug.Log("Player has died.");
        // Handle player death logic
    }

}

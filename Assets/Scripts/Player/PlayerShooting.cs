using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    private InputSystem_Actions mouseInput;
    [SerializeField] private WeaponData currentWeapon;
    [SerializeField] private Transform firePoint;
    private float nextTimeToFire = 0f;
    private UpgradeStats upgradeStats;
    private float finalDamage;
    private float finalBulletSpeed;
    private float finalFireRate;
    private float finalRange;
    private float finalBulletSize = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        mouseInput = new InputSystem_Actions();
        upgradeStats = GetComponent<UpgradeStats>();
        finalBulletSpeed = currentWeapon.bulletSpeed;
        finalDamage = currentWeapon.damage;
        finalFireRate = currentWeapon.fireRate;
        finalRange = currentWeapon.range;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseInput.Player.Attack.IsPressed()&& Time.time >= nextTimeToFire)
        {
            Shoot();
        }
    }

    void OnEnable()
    {
        mouseInput.Player.Enable();
        GameEvents.OnUpgradeApplied += RecalculateStats;

    }

    void OnDisable()
    {
        mouseInput.Player.Disable();
        GameEvents.OnUpgradeApplied -= RecalculateStats;
    }

    private void RecalculateStats()
    {
        finalBulletSpeed = currentWeapon.bulletSpeed * (1 + upgradeStats.GetModifier(UpgradeType.BulletSpeed) /100f);
        finalDamage = currentWeapon.damage * (1 + upgradeStats.GetModifier(UpgradeType.GunDamage) /100f);
        finalFireRate = currentWeapon.fireRate * (1 + upgradeStats.GetModifier(UpgradeType.FireRate) /100f);
        finalRange = currentWeapon.range * (1 + upgradeStats.GetModifier(UpgradeType.GunRange) /100f);
        finalBulletSize = 1f + (upgradeStats.GetModifier(UpgradeType.BulletSize) / 100f);
    }

    void Shoot()
    {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 direction = (mouseWorldPos - (Vector2)firePoint.position).normalized;
            //Gets Mouse position, converts it to world position, and calculates the direction from the fire point to the mouse position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Adjust angle to point upwards
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            //Finds angle for the bullet to rotate towards the mouse position
            GameObject bullet = Instantiate(currentWeapon.bulletPrefab, firePoint.position, rotation);
            bullet.GetComponent<Bullet>().Initialize(finalDamage, finalBulletSpeed, finalRange, direction, finalBulletSize);
            //Instantiates the bullet prefab at the fire point position with the calculated rotation and initializes it with the weapon's damage, bullet speed, range, and direction
            nextTimeToFire = Time.time + 1f / finalFireRate;
            //Sets the next time the player can fire based on the weapon's fire rate (Bullets per second)
    }
}

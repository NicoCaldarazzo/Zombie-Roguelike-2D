using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    private InputSystem_Actions mouseInput;
    [SerializeField] private WeaponData currentWeapon;
    [SerializeField] private Transform firePoint;
    private float nextTimeToFire = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        mouseInput = new InputSystem_Actions();
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

    }

    void OnDisable()
    {
        mouseInput.Player.Disable();
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
            bullet.GetComponent<Bullet>().Initialize(currentWeapon.damage, currentWeapon.bulletSpeed, currentWeapon.range, direction);
            //Instantiates the bullet prefab at the fire point position with the calculated rotation and initializes it with the weapon's damage, bullet speed, range, and direction
            nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
            //Sets the next time the player can fire based on the weapon's fire rate (Bullets per second)
    }
}

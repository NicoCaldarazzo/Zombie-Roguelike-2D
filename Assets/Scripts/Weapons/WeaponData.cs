using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float fireRate;
    public float bulletSpeed;
    public float range;
    public int ammoCapacity;
    public float reloadTime;
    public GameObject bulletPrefab;
    public Sprite weaponSprite;
}

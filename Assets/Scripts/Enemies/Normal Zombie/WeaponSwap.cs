using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
 [SerializeField] private WeaponData currentWeapon;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = currentWeapon.weaponSprite;
    }
}

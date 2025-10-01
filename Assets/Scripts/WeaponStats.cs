using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string name;
    public WeaponType type;
    public int damage = 10;
    public float attackSpeed = 2f;
    public int knockback = 1;
    public int DoT = 0;
    public float spinSpeed = 0;
    public GameObject Thing;
    [Header("Use for Melee Weapon")]
    public int range = 1;
    [Header("Use for Range Weapon")]
    public float spread = 0;
    public int pierce = 0;
    public int projectSpeed = 0;
    public float projectLife = 0;

    public GameObject bullet;
}

public enum WeaponType
{
    Melee,
    Range
}

public class WeaponStats : MonoBehaviour
{
    [Header("Available Weapons")]
    public Weapon[] weapon;
    public Weapon currentWeapon;

    // NEW: reference to hold the instantiated weapon prefab
    private GameObject weaponVisual;

    void Start()
    {
        // Select the current weapon based on GameManager
        for (int i = 0; i < weapon.Length; i++)
        {
            if (GameManager.Instance.Weapon == weapon[i].name)
            {
                currentWeapon = weapon[i];
                break;
            }
        }

        // Minimal addition: instantiate Thing as the weapon visual
        if (currentWeapon != null && currentWeapon.Thing != null)
        {
            weaponVisual = Instantiate(
                currentWeapon.Thing,
                transform.position,
                transform.rotation,
                transform
            );

            weaponVisual.transform.localPosition = Vector3.zero;
            weaponVisual.transform.localRotation = Quaternion.identity;
            weaponVisual.transform.localScale = Vector3.one;
        }
    }

    void Update()
    {
        // Nothing else changed
    }
}

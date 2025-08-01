using UnityEngine;

[System.Serializable]
public class Weapon {
    public string name;
    public WeaponType type;
    public int damage = 10;
    public float attackSpeed = 2f;
    public int knockback = 1;
    [Header("Use for Melee Weapon")]
    public int spinSpeed = 360;
    public int range = 1;
    [Header("Use for Range Weapon")]
    public int projectSpeed = 0;
    public int spread = 0;

}
public enum WeaponType {
    Melee,
    Range
    
}



public class WeaponStats : MonoBehaviour
{
    [Header("Available Weapons")]
    public Weapon[] weapon;
    public Weapon currentWeapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i=0 ; i< weapon.Length; i++)
        {
            if (SceneSwitch.Instance.Weapon == weapon[i].name)
            {
                currentWeapon= weapon[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

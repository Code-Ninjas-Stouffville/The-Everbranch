using UnityEngine;
using static EnemySpawn;

public class EnemyMovement : MonoBehaviour
{
    public WeaponType eType;
    public int eLevel = 0;
    public int eHealth = 0;
    public int eDamage = 0;
    public int eSpeed = 0;
    public int eRange = 0;
    public int eDrop = 0;
    public enum WeaponType
    {
        Melee,
        Range

    }
}

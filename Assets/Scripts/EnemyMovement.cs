using UnityEngine;
using static EnemySpawn;

public class EnemyMovement : MonoBehaviour
{
    public WeaponType eType;
    public int eHealth = 0;
    public int eDamage = 0;
    public float eSpeed = 0;
    public float eRange = 0;
    public int eDrop = 0;
    private GameManager gameManager;
    public enum EnemyType
    {
        Melee,
        Range

    }
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        eHealth = eHealth * gameManager.Round;
        eSpeed = eSpeed + gameManager.Round/10;
        eDrop = eDrop * gameManager.Round;
    }
}

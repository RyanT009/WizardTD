using UnityEngine;

[System.Serializable]
public class EnemyGroup
{
    public GameObject enemyPrefab; // Type of enemy to spawn
    public int count; // Number of enemies to spawn
    public float spawnDelay; // Delay between each enemy spawn
}

[CreateAssetMenu(fileName = "NewWave", menuName = "TowerDefense/Wave")]
public class EnemyWave : ScriptableObject
{
    public EnemyGroup[] enemyGroups; // List containing all groups in a given wave
    public float timeBetweenGroups = 1f;  // Delay between each group spawn
}
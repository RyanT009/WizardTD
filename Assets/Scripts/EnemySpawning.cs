using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public static EnemySpawning instance;
    public static int aliveCount = 0;

    [SerializeField] List<GameObject> checkPointParents; // Parent objects holding checkpoints for each path
    [SerializeField] List<GameObject> enemiesAlive; // List holding all enemies alive
    [SerializeField] TextMeshProUGUI enemiesAliveText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void SpawnEnemy(GameObject enemyPrefab)
    {
        int spawnPointIndex = Random.Range(0, checkPointParents.Count); // Pick a random path
        Vector3 spawnPos = checkPointParents[spawnPointIndex].transform.GetChild(0).position; // Set spawn point to first invisible checkpoint
        spawnPos.y = (enemyPrefab.transform.localScale.y + 3f) / 2f; // Set enemy y pos to be above gorund

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity); // Spawn enemy
        enemy.GetComponent<EnemyMovement>().SetCheckpointParent(checkPointParents[spawnPointIndex]); // Tell enemy what path it is on

        AddEnemy(enemy); // Add to enemiesAlive list
    }

    // Increment alive count, add enemy to alive list, update enemiesAliveText
    void AddEnemy(GameObject enemy)
    {
        aliveCount++;
        enemiesAlive.Add(enemy);

        enemiesAliveText.text = aliveCount.ToString() + " enemies left";
    }

    // Decrement alive count, remove enemy from alive list, update enemiesAliveText
    public void RemoveEnemy(GameObject enemy)
    {
        aliveCount--;
        enemiesAlive.Remove(enemy);

        enemiesAliveText.text = aliveCount.ToString() + " enemies left";
    }


}

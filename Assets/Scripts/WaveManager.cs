using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [SerializeField] EnemyWave[] waves; // List containing all waves
    [SerializeField] EnemySpawning enemySpawner; // Enemy spawning script
    [SerializeField] TextMeshProUGUI waveNumberText;
    [SerializeField] float timeBetweenWaves;

    [SerializeField] TooltipManager tooltipManager;
    [SerializeField] GameManager gameManager;


    private void Start()
    {
        StartCoroutine(StartWaves());
    }
    private void Update()
    {
        /* Start via key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartWaves());
        }
        */
    }
    public IEnumerator StartWaves()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            Debug.Log("Starting Wave " + (i + 1).ToString());
            waveNumberText.text = "WAVE " + (i + 1);

            tooltipManager.CheckForTooltips(i + 1);

            int enemyCount = 0;
            foreach (var group in waves[i].enemyGroups)
            {
                enemyCount += group.count;
            }
            enemySpawner.SetInitialEnemyCount(enemyCount);

            yield return StartCoroutine(SpawnWave(waves[i])); // Call spawning of groups

            yield return new WaitUntil(() => EnemySpawning.aliveCount == 0); // Wait until all enemies are dead

            yield return new WaitForSeconds(timeBetweenWaves); // Wait to start next wave
        }

        Debug.Log("All waves complete!");
        Invoke("WinScreen", 8f);
    }

    IEnumerator SpawnWave(EnemyWave wave)
    {
        foreach (var group in wave.enemyGroups) // Spawn each group, with timeBetweenGroups delay between them
        {
            for (int i = 0; i < group.count; i++) // Spawn each enemy in a group, with spawnDelay delay between them
            {
                enemySpawner.SpawnEnemy(group.enemyPrefab);
                yield return new WaitForSeconds(group.spawnDelay);
            }

            yield return new WaitForSeconds(wave.timeBetweenGroups);
        }

        gameManager.ChangeSkillPoints(1);
    }

    void WinScreen()
    {
        SceneManager.LoadScene(3);
    }
}

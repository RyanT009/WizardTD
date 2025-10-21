using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] List<GameObject> checkPointParents;
    [SerializeField] GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int spawnPointIndex = Random.Range(0, checkPointParents.Count);
            Vector3 spawnPos = checkPointParents[spawnPointIndex].transform.GetChild(0).position;
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}

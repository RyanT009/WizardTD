using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    public float enemyDistance;
    [SerializeField] float moveSpeed;
    EnemyBehaviour enemyBehaviourScript;

    [SerializeField] GameObject checkpointParent; // Initial GameObject holding all checkpoints needed to copy over into allCheckpoints
    private List<GameObject> allCheckpoints = new List<GameObject>(); // List holding all checkpoints for this enemy's path
    private GameObject nextCheckpoint; // Next checkpoint GameObject to move towards
    [SerializeField] int currentCheckpointIndex; // Index holding what checkpoint the enemy needs to move to next


    


    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviourScript = GetComponent<EnemyBehaviour>();

        currentCheckpointIndex = 0;
        PopulateCheckpoints();   
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDistance();
        
        if (currentCheckpointIndex < allCheckpoints.Count) // If not at the end of the path
        {
            Vector3 targetPos = nextCheckpoint.transform.position;
            targetPos.y = transform.position.y;
            Vector3 direction = targetPos - transform.position;

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            if (Vector3.Distance(transform.position, targetPos) < 0.001f)
            {
                AdvanceCheckpoint();
            }
        }

    }

    // EnemySpawning tells this enemy what path they are on by assigning the correct checkpointParent
    public void SetCheckpointParent(GameObject parent)
    {
        checkpointParent = parent;
    }

    void PopulateCheckpoints()
    {
        if (checkpointParent == null)
        {
            Debug.Log("Checkpoint parent not set");
            return;
        }

        // Populate allCheckpoints by copying over checkpointParent's children
        foreach (Transform child in checkpointParent.transform)
        {
            allCheckpoints.Add(child.gameObject);
        }

        nextCheckpoint = allCheckpoints[currentCheckpointIndex]; // Set next checkpoint
    }

    // Increment currentCheckpointIndex if not yet at the end of the path
    void AdvanceCheckpoint()
    {
        currentCheckpointIndex++;

        if (currentCheckpointIndex < allCheckpoints.Count)
        {
            nextCheckpoint = allCheckpoints[currentCheckpointIndex];
        }
        else
        {
            // Kill the enemy when it hits the fortress
            enemyBehaviourScript.Death(true);
        }
    }

    // Call function to remove enemy from alive list
   

    public float GetDistance()
    {
        return enemyDistance;
    }
    private void CalculateDistance()
    {
        Vector3 targetPos = nextCheckpoint.transform.position;
        targetPos.y = transform.position.y;

        enemyDistance = (currentCheckpointIndex * 1000) - Vector3.Distance(transform.position, targetPos);
    }
}

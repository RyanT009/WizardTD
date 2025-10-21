using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject checkpointParent;
    private List<GameObject> allCheckpoints;
    private GameObject nextCheckpoint;
    private int currentCheckpointIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentCheckpointIndex = 0;
        PopulateCheckpoints();   
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCheckpointIndex < allCheckpoints.Count)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                nextCheckpoint.transform.position,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, nextCheckpoint.transform.position) < 0.001f)
            {
                AdvanceCheckpoint();
            }
        }
    }

    void PopulateCheckpoints()
    {
        foreach (Transform child in checkpointParent.transform)
        {
            allCheckpoints.Add(child.gameObject);
        }

        nextCheckpoint = allCheckpoints[currentCheckpointIndex];
    }

    void AdvanceCheckpoint()
    {
        currentCheckpointIndex++;

        if (currentCheckpointIndex < allCheckpoints.Count)
        {
            nextCheckpoint = allCheckpoints[currentCheckpointIndex];
        }
        else
        {
            Debug.Log("Last checkpoint reached");
        }
    }
}

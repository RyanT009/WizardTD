using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretRotation : MonoBehaviour
{
    private GameObject enemy = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            Vector3 targetPosition = enemy.transform.position; //Get position of enemy
            targetPosition.y = transform.position.y; //Set y axis to same as turret to lock x and z rotation
            transform.LookAt(targetPosition);
        }

        
    }

    public void SetTarget(GameObject target)
    {
        enemy = target;
    }
}

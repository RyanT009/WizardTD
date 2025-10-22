using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretTargeting : MonoBehaviour
{

    [SerializeField] float range;
    [SerializeField] float shootTime;
    [SerializeField] float damage;
    private float shootTimer;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject currentTarget;
    private SphereCollider targetingField;
    [SerializeField] List<GameObject> enemiesInRange;
    [SerializeField] turretRotation rotationScript;
    


    // Start is called before the first frame update
    void Start()
    {
        targetingField = GetComponent<SphereCollider>();

        targetingField.radius = range;

        
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0 && currentTarget != null)
        {
            shootTimer = shootTime;
            Shoot();
        }
    }
    
    private void OnEnable()
    {
        EnemyMovement.OnEnemyDestroyed += HandleEnemyDestroyed;
    }

    private void OnDisable()
    {
        EnemyMovement.OnEnemyDestroyed -= HandleEnemyDestroyed;
    }

    void OnTriggerEnter(Collider other) // Triggers when entering range
    {
        if (other.tag == "enemy")
        {
            enemiesInRange.Add(other.gameObject);
            TargetSelect();
        }

        
    }

    void OnTriggerExit(Collider other) // Triggers when exiting range
    {
        if (other.tag == "enemy")
        {
            enemiesInRange.Remove(other.gameObject);
            TargetSelect();
        }

    }
    
    void HandleEnemyDestroyed(EnemyMovement destroyedEnemy)
    {
        Debug.Log("detected");
        enemiesInRange.Remove(destroyedEnemy.gameObject);
        TargetSelect();
    }

    void TargetSelect()
    {
        GameObject frontEnemy = null;
        float highestDistance = 0f;


        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            if (i >= enemiesInRange.Count)
            {
                break;
            }

            if (enemiesInRange[i] != null)
            {
                float currentDistance = enemiesInRange[i].GetComponent<EnemyMovement>().GetDistance();

                if (currentDistance > highestDistance)
                {
                    highestDistance = currentDistance;
                    frontEnemy = enemiesInRange[i];
                }
            }
        }

        currentTarget = frontEnemy;
        rotationScript.SetTarget(frontEnemy);
    }
    
    void RemoveNullEnemies()
    {
        enemiesInRange.RemoveAll(item => item == null);
    }
    
    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<ProjectileMovement>().SetTarget(currentTarget);
    }
}

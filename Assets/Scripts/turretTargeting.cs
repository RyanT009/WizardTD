using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargeting : MonoBehaviour
{

    [SerializeField] float range;
    [SerializeField] float shootTime;
    [SerializeField] float damage;
    private float shootTimer;

    [SerializeField] int type; //0 = mage, 1 = cannon, 2 = tesla
    [SerializeField] int level;


    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Vector3 projectileSpawnOffset;

    [SerializeField] GameObject currentTarget;

    [SerializeField] string turretType = null;

    private SphereCollider targetingField;
    [SerializeField] List<GameObject> enemiesInRange;
    private TurretRotation rotationScript;

    [SerializeField] UpgradeManager upgradeManager;
    

    // Start is called before the first frame update
    void Start()
    {
        upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();

        level = 1;

        damage = upgradeManager.getTowerStats()[type][level - 1][0];
        shootTime = 1f / upgradeManager.getTowerStats()[type][level - 1][1];
        range = upgradeManager.getTowerStats()[type][level - 1][2];

        targetingField = GetComponent<SphereCollider>();
        targetingField.radius = range;

        rotationScript = GetComponent<TurretRotation>();

        
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

    public void UpgradeTower(Vector3 newStats)
    {
        damage = newStats.x;
        shootTime = 1f / newStats.y;
        range = newStats.z;

        GetComponent<SphereCollider>().radius = range;

        level++;
    }

    public Vector2Int GetTypeAndLevel()
    {
        return new Vector2Int(type, level);
    }

    public GameObject GetTarget()
    {
        return currentTarget;
    }

    private void OnEnable()
    {
        EnemyBehaviour.OnEnemyKilled += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyBehaviour.OnEnemyKilled -= HandleEnemyKilled;
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
    
    void HandleEnemyKilled(EnemyBehaviour killedEnemy)
    {
        //Debug.Log("detected");
        enemiesInRange.Remove(killedEnemy.gameObject);
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

        if (rotationScript != null)
        {
            rotationScript.SetTarget(frontEnemy);
        }
    }

    void RemoveNullEnemies()
    {
        enemiesInRange.RemoveAll(item => item == null);
    }

    void Shoot()
    {
        // Start at turret position
        Vector3 spawnPos = transform.position;

        if (rotationScript != null)
        {
            Quaternion barrelRot = rotationScript.GetObjectRotation();

            // Apply rotation only to the forward (Z) and sideways (X) offsets
            Vector3 horizontalOffset = new Vector3(projectileSpawnOffset.x, 0f, projectileSpawnOffset.z);
            spawnPos += barrelRot * horizontalOffset;

            // Apply vertical offset separately
            spawnPos.y += projectileSpawnOffset.y;
        }
        else
        {
            // fallback if no rotationScript
            spawnPos += projectileSpawnOffset;
        }

        // Spawn projectile unrotated
        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        // Pass target and damage
        Projectile projScript = projectile.GetComponent<Projectile>();
        projScript.SetType(turretType);
        projScript.SetTarget(currentTarget);
        projScript.PassDamage(damage);
    }



    // Add this to your existing turretTargeting class
    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }
}

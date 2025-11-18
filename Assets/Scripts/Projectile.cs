using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float movementSpeed;
    [SerializeField] float damage;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // If the projectile has a target, calculate the direction and move towards the target
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            rb.velocity = direction * movementSpeed;
        }
        else // If enemy is destroyed for whatever reason
        {
            Destroy(gameObject); //Destroy projectile
        }
    }


    public void SetTarget(GameObject enemy)
    {
        target = enemy;
    }
    
    public void PassDamage(float turretDamage)
    {
        damage = turretDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        // If the projectile hits the target, damage it
        if (other.gameObject == target)
        {
            EnemyBehaviour behaviourScript = target.GetComponent<EnemyBehaviour>();
            behaviourScript.TakeDamage(damage); //Call function within enemyBehaviour

            Destroy(gameObject);
        }
    }

    // If the projectile leaves the map
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        EnemyBehaviour.OnEnemyKilled += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyBehaviour.OnEnemyKilled -= HandleEnemyKilled;
    }

    // broken
    void HandleEnemyKilled(EnemyBehaviour killedEnemy)
    {
        Debug.Log("PROJ KILLED1");
        if (killedEnemy == target)
        {
            Debug.Log("PROJ KILLED2");
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float movementSpeed;
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
    }

    public void SetTarget(GameObject enemy)
    {
        target = enemy;
    }

    void OnTriggerEnter(Collider other)
    {
        // If the projectile hits the target, destroy it
        if (other.gameObject == target)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    // If the projectile leaves the map
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

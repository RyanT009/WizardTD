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

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
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
        if (other.gameObject == target)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if(other.tag == "border")
        {
            Destroy(gameObject);
        }
    }
}

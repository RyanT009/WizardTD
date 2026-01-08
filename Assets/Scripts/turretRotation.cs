using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    private GameObject enemy = null;
    [SerializeField] GameObject objectToRotate;

    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = objectToRotate.transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            Vector3 targetPosition = enemy.transform.position;
            targetPosition.y = objectToRotate.transform.position.y; // Lock y axis

            // Calculate rotation towards target
            Quaternion lookRotation = Quaternion.LookRotation(targetPosition - objectToRotate.transform.position) * initialRotation;

            // Apply initial rotation as an offset

            objectToRotate.transform.rotation = new Quaternion(0, lookRotation.y, 0, 0);
        }
    }

    public Quaternion GetObjectRotation()
    {
        return objectToRotate.transform.rotation;
    }


    public void SetTarget(GameObject target)
    {
        enemy = target;
    }

    public void ChangeObjectToRotate(GameObject obj)
    {
        initialRotation = obj.transform.localRotation;
        objectToRotate = obj;
    }
}

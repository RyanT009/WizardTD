using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretRotation : MonoBehaviour
{
    private GameObject enemy = null;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("enemy");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(enemy.transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform target;
    public UnityEngine.UI.Image fill;
    private Vector3 offset;
    private Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) //If target is still alive
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);
            screenPos.y += offset.y;
            transform.position = screenPos;
        }
        else //When target is removed from game, remove health bar
        {
            Destroy(gameObject);
        }
        
    }

    public void Initialize(Transform enemy, Vector3 offsetPos)
    {
        target = enemy;
        offset = offsetPos;
        mainCamera = Camera.main;
    }
    
    public void setHealth(float currentHealth, float maxHealth)
    {
        fill.fillAmount = currentHealth / maxHealth;
    }
}

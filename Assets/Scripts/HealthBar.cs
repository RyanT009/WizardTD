using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private Transform target;
    [SerializeField] bool followTarget;
    [SerializeField] bool isVisible;

    private UnityEngine.UI.Image border;
    private UnityEngine.UI.Image fill;
    private Vector3 offset;
    private Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        border = GetComponent<Image>();
        fill = transform.GetChild(0).GetComponent<Image>();

        if (!isVisible)
        {
            border.enabled = false;
            fill.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) //If target is still alive
        {
            if (followTarget)
            {
                transform.position = target.position + offset;
            }
        }
        
    }

    public void Initialize(Transform enemy, Vector3 offsetPos)
    {
        target = enemy;
        followTarget = true;
        offset = offsetPos;
        mainCamera = Camera.main;
        transform.localRotation = Quaternion.identity;
    }
    
    public void SetHealth(float currentHealth, float maxHealth)
    {
        isVisible = true;
        fill.fillAmount = currentHealth / maxHealth;

        if (fill.fillAmount < 1f)
        {
            border.enabled = true;
            fill.enabled = true;
        }
    }

    public void KillHealthbar()
    {
        Destroy(gameObject);
    }
}

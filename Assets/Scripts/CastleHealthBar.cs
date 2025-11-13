using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CastleHealthBar : MonoBehaviour
{
    public Transform target;
    public UnityEngine.UI.Image fill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void setHealth(float currentHealth, float maxHealth)
    {
        fill.fillAmount = currentHealth / maxHealth;
    }
}

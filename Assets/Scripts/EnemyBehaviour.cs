using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float maxHealth;
    private float currentHealth;
    [SerializeField] HealthBar healthBarPrefab;
    private HealthBar thisHealthBar;
    [SerializeField] CastleManager castle;
    [SerializeField] GameManager gameManager;

    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        Canvas canvas = FindFirstObjectByType<Canvas>();
        thisHealthBar = Instantiate(healthBarPrefab, canvas.transform);
        thisHealthBar.Initialize(transform, new Vector3(0,20F,0));

        gameManager = FindFirstObjectByType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void damageTaken(float damage)
    {
        if (currentHealth - damage <= 0)
        {
            death();
        }
        else
        {
            currentHealth -= damage;
            thisHealthBar.setHealth(currentHealth, maxHealth);
        }
    }
    
    public void death()
    {
        gameManager.ChangeMoney(100);
        Destroy(gameObject);
    }
}

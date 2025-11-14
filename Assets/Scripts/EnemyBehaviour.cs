using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float maxHealth;
    private float currentHealth;
    [SerializeField] HealthBar healthBarPrefab;
    private HealthBar thisHealthBar;
    [SerializeField] CastleManager castle;
    [SerializeField] GameManager gameManager;
    [SerializeField] Animator animator;
    public Canvas canvas;
    public static event Action<EnemyBehaviour> OnEnemyKilled; // A system event to trigger when enemy destroyed


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
            currentHealth -= damage;
            thisHealthBar.setHealth(currentHealth, maxHealth);
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
        Debug.Log("death");
        gameManager.ChangeMoney(100); // Increase money
        OnEnemyKilled?.Invoke(this); // Trigger event
        EnemySpawning.instance.RemoveEnemy(gameObject); // Remove
        GetComponent<EnemyMovement>().enabled = false; //Disable movement
        animator.SetBool("Death", true);
        Invoke("FadeOut",0f);
        //Destroy(gameObject);
    }

    void FadeOut()
    {
        Destroy(gameObject,2f);
    }
}

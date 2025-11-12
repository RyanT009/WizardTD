using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    public Canvas canvas;
    [SerializeField] float maxHealth;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject enemy = collision.gameObject;
        enemy.GetComponent<EnemyBehaviour>().castleReached();
        damageTaken(20);
    }

    public void damageTaken(float damage)
    {
        if (currentHealth - damage <= 0)
        {
            GameOver();
        }
        else
        {
            currentHealth -= damage;
            healthBar.setHealth(currentHealth, maxHealth);
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER!");
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    [SerializeField] CastleHealthBar healthBar;
    public Canvas canvas;
    [SerializeField] float maxHealth;
    public float currentHealth;
    // Start is called before the first frame update
    void Start(){
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        damageTaken(20f);
        Debug.Log("Castle took damage!");
        Destroy(other.gameObject);
        //^ this doesnt work
    }


    private void damageTaken(float damage)
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

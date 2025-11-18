using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    [SerializeField] int moneyWorth;
    [SerializeField] int damageToCastle;

    [SerializeField] HealthBar healthBarPrefab;
    private HealthBar thisHealthBar;

    [SerializeField] CastleManager castle;
    [SerializeField] GameManager gameManager;
    [SerializeField] Animator animator;

    public GameObject goldCoinPrefab; // Spinning Gold Coin
    [SerializeField] float coinSpinDuration = 1f; // Spin Duration
    [SerializeField] float coinFadeDelay = 2f;   // Fade Delay

    public static event Action<EnemyBehaviour> OnEnemyKilled; // A system event to trigger when enemy destroyed

    [SerializeField] float sinkDelay; // How long after dying to start sinking
    [SerializeField] float sinkSpeed; // How fast to sink



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        GameObject worldCanvas = GameObject.FindGameObjectWithTag("worldCanvas");

        thisHealthBar = Instantiate(healthBarPrefab, worldCanvas.transform);
        thisHealthBar.Initialize(transform, new Vector3(0f, 20f, -4f));
        thisHealthBar.transform.localScale *= 0.4f;

        gameManager = FindFirstObjectByType<GameManager>();
        castle = FindFirstObjectByType<CastleManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        Debug.Log("DAMAGE: " + damage);
        currentHealth -= damage;
        thisHealthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Death(false);
        }
    }
    
    public void Death(bool hitCastle)
    {
        Debug.Log("death");

        if (!hitCastle) // If player killed the enemy
        {
            gameManager.ChangeMoney(moneyWorth); // Increase money

            //SpawnGoldCoin(); // Spawns a coin next to the dead enemy
        }
        else // If enemy hit the castle
        {
            currentHealth = 0;
            castle.TakeDamage(damageToCastle);
        }

        OnEnemyKilled?.Invoke(this); // Trigger kill event for turrets to stop targeting
        EnemySpawning.instance.RemoveEnemy(gameObject); // Remove from enemy spawner / total enemy count

        thisHealthBar.KillHealthbar(); // Destroy health bar

        GetComponent<EnemyMovement>().enabled = false; //Disable movement
        animator.SetBool("Death", true); // Trigger death animation

        StartSink(); // Animate model to sink through floor
    }

    /*
    void SpawnGoldCoin(){

        if (goldCoinPrefab == null){
            Debug.LogError("goldCoinPrefab is still null!");
            return;
        }

        // Spawn coin
        GameObject coin = Instantiate(goldCoinPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);

        // Spin  coin for n duration
        StartCoroutine(SpinCoin(coin.transform, coinSpinDuration));

        // Destroy the coin after fading
        Destroy(coin, coinFadeDelay);
    }

    IEnumerator SpinCoin(Transform coin, float duration){
        float elapsed = 0f;
        while (elapsed < duration){
            coin.Rotate(0, 360 * Time.deltaTime / duration, 0); // Spins around Y axis
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    */

    public bool AliveCheck()
    {
        if (currentHealth <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // To be implemented
    void StartSink()
    {
        InvokeRepeating("Sink", sinkDelay, 1f / sinkSpeed);
    }

    void Sink()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);

        if (transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
    }
}

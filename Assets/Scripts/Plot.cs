using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour {

    [SerializeField] GameObject towerPrefab; // What tower is being placed
    [SerializeField] Vector3 towerOffset; // Tower placement offset from the plot
    private bool isEmpty = true; // Is the plot empty
    private GameObject placedTower; // Reference to the placed tower
    private int currentLevel = 1; // Current Tower Level

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    void OnMouseDown() { // when the user clicks on their mouse...


        if (isEmpty == false) // If the plot has a tower on it
        {
            TowerUIManager.Instance.ShowUpgradeUI(this); // Show upgrade menu
            //Debug.Log("Error: This plot already has a tower in it!"); // sends an error message to the console
            return;
        }

        if (gameManager.GetCurrency() >= 50)
        {
            // If the plot is empty
            Vector3 spawnPosition = transform.position + towerOffset; // Calculate tower spawn position
            placedTower = Instantiate(towerPrefab, spawnPosition, Quaternion.identity); // Spawn the tower on the plot
            placedTower.transform.SetParent(transform, true); // Makes the tower a child of the plot so if the plot is deleted, the tower will be deleted with it.
            isEmpty = false; // Flag the plot as now no longer empty so that no new towers can be placed on it

            gameManager.ChangeMoney(-50);

            TowerUIManager.Instance.HideUpgradeUI(); // Hide upgrade menu
        }

        //Debug.Log("Success: Tower placed in the plot!"); // Confirms tower placement by printing confirmation in console
    }
//    public void UpgradeTower()
//    {
//        if (placedTower != null) // If there is a tower, upgrade it
//        {
//            turretTargeting targeting = placedTower.GetComponent<turretTargeting>();
//            if (targeting != null)
//            {
//                targeting.SetDamage(20f); // Upgraded damage
//
//               // Visual feedback
//                Renderer renderer = placedTower.GetComponentInChildren<Renderer>();
//                if (renderer != null)
//                {
//                    renderer.material.color = Color.yellow;
//                }
//
//                Debug.Log("Tower upgraded!");
//            }
//        }
//    }

    public void UpgradeTower(){

        if (placedTower != null){ // If there is a tower
            turretTargeting targeting = placedTower.GetComponent<turretTargeting>();
            
            if (targeting != null){
                
                // Increase damage based on level
                if (currentLevel == 1 && gameManager.GetCurrency() >= 100){
                    targeting.SetDamage(20f); // Level 2 damage
                    SwitchTowerModel(2);
                    currentLevel = 2;
                    gameManager.ChangeMoney(-100);
                }

                else if (currentLevel == 2 && gameManager.GetCurrency() >= 200)
                {
                    targeting.SetDamage(30f); // Level 3 damage
                    SwitchTowerModel(3);
                    currentLevel = 3;
                    gameManager.ChangeMoney(-200);
                }

                else{
                    Debug.Log("Error: Tower already at max level!");
                    return;
                }

                Debug.Log("Tower upgraded to level " + currentLevel + "!");
            }
        }

    }
    public void DowngradeTower(){
        if (placedTower != null){

            turretTargeting targeting = placedTower.GetComponent<turretTargeting>();

            if (targeting != null){

                // Increase damage based on level
                if (currentLevel == 2){
                    targeting.SetDamage(10f); // Level 1 damage
                    SwitchTowerModel(1);
                    currentLevel = 1;
                }

                else if (currentLevel == 3){
                    targeting.SetDamage(20f); // Level 2 damage
                    SwitchTowerModel(2);
                    currentLevel = 2;
                }

                else{
                    Debug.Log("Error: Tower already at the lowest level!");
                    return;
                }

                Debug.Log("Tower downgraded to level " + currentLevel + "!");
            }
        }
    }
    private void SwitchTowerModel(int level){
        // Find all the models
        Transform level1 = placedTower.transform.Find("ArcherTower");
        Transform level2 = placedTower.transform.Find("Cannon");
        Transform level3 = placedTower.transform.Find("TeslaTower");

        // Disable all first
        if (level1 != null) {
            level1.gameObject.SetActive(false);
        }

        if (level2 != null) {
            level2.gameObject.SetActive(false);
        }

        if (level3 != null) {
            level3.gameObject.SetActive(false);
        }

        // Enable only the one that is for the new level
        if (level == 1 && level1 != null){
            level1.gameObject.SetActive(true);
        }

        else if (level == 2 && level2 != null){
            level2.gameObject.SetActive(true);
        }

        else if (level == 3 && level3 != null){
            level3.gameObject.SetActive(true); 
        }

    }

    public void DestroyTower()
    {
        if (placedTower != null) // If there is a tower, destroy it and reset plot
        {
            Destroy(placedTower);
            placedTower = null;
            isEmpty = true;
            Debug.Log("Tower destroyed!");

            gameManager.ChangeMoney(50);
        }
    }
}
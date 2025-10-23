using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour {

    [SerializeField] GameObject towerPrefab; // What tower is being placed
    [SerializeField] Vector3 towerOffset; // Tower placement offset from the plot
    private bool isEmpty = true; // Is the plot empty
    private GameObject placedTower; // Reference to the placed tower

    void OnMouseDown(){ // when the user clicks on their mouse...


        if (isEmpty == false) // If the plot has a tower on it
        {
            TowerUIManager.Instance.ShowUpgradeUI(this); // Show upgrade menu
            //Debug.Log("Error: This plot already has a tower in it!"); // sends an error message to the console
            return;
        }

        // If the plot is empty
        Vector3 spawnPosition = transform.position + towerOffset; // Calculate tower spawn position
        placedTower = Instantiate(towerPrefab, spawnPosition, Quaternion.identity); // Spawn the tower on the plot
        placedTower.transform.SetParent(transform, true); // Makes the tower a child of the plot so if the plot is deleted, the tower will be deleted with it.
        isEmpty = false; // Flag the plot as now no longer empty so that no new towers can be placed on it

        TowerUIManager.Instance.HideUpgradeUI(); // Hide upgrade menu
        //Debug.Log("Success: Tower placed in the plot!"); // Confirms tower placement by printing confirmation in console
    }
    public void UpgradeTower()
    {
        if (placedTower != null) // If there is a tower, upgrade it
        {
            turretTargeting targeting = placedTower.GetComponent<turretTargeting>();
            if (targeting != null)
            {
                targeting.SetDamage(20f); // Upgraded damage

                // Visual feedback
                Renderer renderer = placedTower.GetComponentInChildren<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.yellow;
                }

                Debug.Log("Tower upgraded!");
            }
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
        }
    }
}

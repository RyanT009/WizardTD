using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // so this is for seeing if the mouse is clicking on the plot square

public class Plot : MonoBehaviour{

    public GameObject tower; // links to the tower like a turret
    public Vector3 extraHeight = new Vector3(0f, 0.5f, 0f); // gives the tower a height boost to sit on top of the plot

    private bool isEmpty = true; // the status of the plot i.e. does it have a tower on it?
    private GameObject placedTower; // creates a reference to the placed tower so it can be upgraded/destroyed later if needed

    void OnMouseDown(){ // when the user clicks on their mouse...
        if (isEmpty == false){ // if the plot is not free
            Debug.Log("Error: This plot already has a tower in it!"); // sends an error message to the console
            return; // stops the rest of the function from running
        }

        Vector3 spawnPosition = transform.position + extraHeight; // place down the tower ON the plot
        placedTower = Instantiate(tower, spawnPosition, Quaternion.identity); // clone the prefab tower in spawnPositon coordiantes. Quaternion.identity stops the turret from rotating and is needed to stop errors

        placedTower.transform.SetParent(transform, true); // makes the tower a child of the plot so if the plot is deleted, the tower will be deleted with it.

        isEmpty = false; // flag the plot as now no longer empty so that no new towers can be placed on it

        Debug.Log("Success: Tower placed in the plot!"); // confirms tower placement by printing confirmation in console
    }
}

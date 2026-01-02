using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{

    [SerializeField] bool isEmpty = true;
    [SerializeField] GameObject placedTower;
    [SerializeField] UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void PlaceTowerHere(GameObject prefab, Vector3 offset)
    {
        if(!isEmpty)
        {
            return;
        }
        
        placedTower = Instantiate(prefab, transform.position + offset, Quaternion.identity);
        isEmpty = false;
    }

    public void SelectThisPlot()
    {
        if (isEmpty)
        {
            uiManager.ShowBuyPanel();
            return;
        }

        

        Vector2Int towerTypeAndLevel = placedTower.GetComponent<TurretTargeting>().GetTypeAndLevel();
        uiManager.SetPlotSelected(transform.GetSiblingIndex());
        uiManager.ShowTowerPanel();
        uiManager.UpdateTowerPanel(towerTypeAndLevel.x, towerTypeAndLevel.y);
    }

    public GameObject GetTower()
    {
        return placedTower;
    }

    public void RemoveTower()
    {
        Destroy(placedTower);
        placedTower = null;

        isEmpty = true;

    }

}
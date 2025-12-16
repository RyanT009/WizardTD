using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{

    [SerializeField] bool isEmpty = true;
    [SerializeField] GameObject placedTower;

    private void Start()
    {

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

}
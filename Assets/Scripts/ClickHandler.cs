using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Exclude the "IgnoreClick" layer from the raycast
        int layerMask = ~(1 << LayerMask.NameToLayer("IgnoreClick"));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            Debug.Log("Clicked: " + hit.collider.gameObject.name);
        }
    }
}

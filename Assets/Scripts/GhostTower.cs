using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTower : MonoBehaviour
{
    [Header("Prefabs & Layers")]
    [SerializeField] GameObject ghostPrefab;       // Assign your ghost tower prefab
    [SerializeField] LayerMask groundMask;         // Assign the layer your terrain is on

    [Header("Settings")]
    [SerializeField] float hoverHeight = 0.1f;     // How much the ghost hovers above terrain
    [SerializeField] float rayHeight = 50f;        // How high above terrain to start raycast
    [SerializeField] float rayLength = 200f;       // How far down to raycast

    void Update()
    {
        FollowMouse();
    }

    void FollowMouse()
    {
        // Step 1: Get mouse position in world XZ
        Vector3 mouseScreen = Input.mousePosition;

        // For orthographic camera, z does not matter
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, 0));

        // Step 2: Cast a ray straight down from above to find terrain height
        Ray ray = new Ray(new Vector3(mouseWorld.x, rayHeight, mouseWorld.z), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, groundMask))
        {
            Vector3 targetPos = hit.point;
            targetPos.y += hoverHeight; // Hover slightly above terrain
            transform.position = targetPos;
        }
    }
}

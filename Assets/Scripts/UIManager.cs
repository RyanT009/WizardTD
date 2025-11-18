using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // plot buttons currently useless
    [SerializeField] GameObject plotButtonPrefab;
    [SerializeField] GameObject plotButtonParent;
    [SerializeField] GameObject plotParent;

    [SerializeField] List<int> towerPrices = new List<int>();
    [SerializeField] List<TextMeshProUGUI> towerPriceTexts = new List<TextMeshProUGUI>();
    [SerializeField] List<Button> towerButtons = new List<Button>();

    [SerializeField] List<GameObject> towerPrefabs = new List<GameObject>();

    [SerializeField] List<GameObject> ghostPrefabs = new List<GameObject>();

    [Header("Ghost Settings")]
    public LayerMask groundMask;
    public float hoverHeight = 0.1f;
    public float rayHeight = 50f;
    public float rayLength = 200f;

    [SerializeField] private float snapRadius; // How close to snap
    [SerializeField] private string plotTag;

    [SerializeField] GameObject currentGhost;
    [SerializeField] int currentGhostIndex = -1;
    [SerializeField] private List<int> ghostOffsets;
    private GameObject snappedPlot;

    private Ray debugRay;


    // Start is called before the first frame update
    void Start()
    {
        CreatePlotButtons();
        SetTowerPrices();
    }

    void Update()
    {
        if (currentGhost != null)
        {
            MoveGhostWithMouse();

            if (snappedPlot != null && Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
    }

   void SpawnGhost(GameObject ghostPrefab)
    {
        if (currentGhost != null) Destroy(currentGhost);

        currentGhost = Instantiate(ghostPrefab);
    }

    private void MoveGhostWithMouse()
    {
        if (currentGhost == null || plotParent == null) return;

        // Cast a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        debugRay = ray;

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, groundMask))
        {
            Vector3 offset = new Vector3(0, ghostOffsets[currentGhostIndex], 0);
            Vector3 targetPos = hit.point + offset;

            targetPos.y += hoverHeight;

            // --- Snap to nearest plot from plotParent children ---
            float closestDist = Mathf.Infinity;
            Transform closestPlot = null;

            foreach (Transform plot in plotParent.transform)
            {
                float dist = Vector3.Distance(targetPos, plot.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestPlot = plot;
                }
            }

            // Snap if within snap radius
            if (closestPlot != null && closestDist <= snapRadius)
            {
                snappedPlot = closestPlot.gameObject;
                targetPos = closestPlot.position + offset;
                targetPos.y += hoverHeight;
            }
            else
            {
                snappedPlot = null;
            }

            // Move ghost
            currentGhost.transform.position = targetPos;

            if (snappedPlot != null)
            {
                SetGhostColor(new Color(0f, 1f, 0f, 0.5f));
            }
            else
            {
                SetGhostColor(new Color(1f, 0f, 0f, 0.5f));
            }
        }
    }

    private void SetGhostColor(Color color)
    {
        Renderer[] renderers = currentGhost.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            // Create a new material instance for this renderer
            Material[] mats = rend.materials; // this automatically instantiates the materials
            for (int i = 0; i < mats.Length; i++)
            {
                Material mat = mats[i];

                // Make sure the material is transparent
                mat.SetFloat("_Mode", 3); // Transparent
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;

                // Set the color with alpha
                mat.color = color;

                mats[i] = mat;
            }
            rend.materials = mats;
        }
    }



    /*
    private void OnDrawGizmos()
    {
        // Draw the debug ray in the scene
        Gizmos.color = Color.green;
        Gizmos.DrawLine(debugRay.origin, debugRay.origin + debugRay.direction * rayLength);

        // Optional: draw a small sphere at the hit point
        if (Physics.Raycast(debugRay, out RaycastHit hit, rayLength, groundMask))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hit.point, 0.3f);
        }
    }
    */



    void CreatePlotButtons()
    {
        foreach (Transform child in plotParent.transform)
        {
            GameObject button = Instantiate(plotButtonPrefab, plotButtonParent.transform);
            button.transform.position = Camera.main.WorldToScreenPoint(child.position);
            button.SetActive(false);
        }
    }

    void SetTowerPrices()
    {
        for (int i = 0; i < towerPrices.Count; i++)
        {
            towerPriceTexts[i].text = "$" + towerPrices[i];
        }
    }

    public void UpdateShopUI(int currentMoney)
    {
        for (int i = 0; i < towerPrices.Count; i++)
        {
            if (currentMoney >= towerPrices[i])
            {
                towerPriceTexts[i].color = Color.yellow;
                towerButtons[i].interactable = true;
            }
            else
            {
                towerPriceTexts[i].color = Color.red;
                towerButtons[i].interactable = false;
            }
        }
    }

    public void CreateGhost(int towerNumber)
    {
        currentGhostIndex = towerNumber;
        SpawnGhost(ghostPrefabs[towerNumber]);
    }

    void PlaceTower()
    {
        Vector3 offset = new Vector3(0, ghostOffsets[currentGhostIndex] + hoverHeight, 0);
        snappedPlot.GetComponent<Plot>().PlaceTowerHere(towerPrefabs[currentGhostIndex], offset);

        snappedPlot = null;
        currentGhostIndex = -1;
        Destroy(currentGhost);

    }
}

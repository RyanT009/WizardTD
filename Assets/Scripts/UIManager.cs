using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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

    // PANELS
    [SerializeField] GameManager gameManager;
    [SerializeField] UpgradeManager upgradeManager;

    [SerializeField] int currentPlotSelected;
    [SerializeField] GameObject rangePrefab;
    [SerializeField] GameObject currentPlotRange;

    [SerializeField] GameObject buyPanel;
    [SerializeField] GameObject towerPanel;

    [SerializeField] TextMeshProUGUI plotNumber;
    [SerializeField] GameObject plotTowerIcon;
    [SerializeField] TextMeshProUGUI plotTowerName;
    [SerializeField] TextMeshProUGUI plotTowerLevel;

    [SerializeField] List<TextMeshProUGUI> plotTowerStats;
    [SerializeField] List<TextMeshProUGUI> plotTowerNextStats;

    [SerializeField] Button upgradeButton;
    [SerializeField] Button sellButton;

    [SerializeField] TextMeshProUGUI plotTowerDescription;



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

            if (Input.GetMouseButtonDown(0))
            {
                if (snappedPlot != null)
                {
                    PlaceTower();
                }
                else
                {
                    CancelPlaceTower();
                }
            }
        }

        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("HOVERING OVER : " + hit.collider.gameObject);
        }
        */
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

            button.GetComponent<Button>().onClick.AddListener(() => SelectAPlot());
            //button.SetActive(false);
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

        gameManager.ChangeMoney(-towerPrices[currentGhostIndex]);

        snappedPlot = null;
        currentGhostIndex = -1;
        Destroy(currentGhost);

    }

    void CancelPlaceTower()
    {
        snappedPlot = null;
        currentGhostIndex = -1;
        Destroy(currentGhost);

    }

    public void SelectAPlot()
    {
        if (currentGhost == null)
        {
            Debug.Log("PLOT HAS BEEN SELECTED");
            // Get the button GameObject that was clicked
            GameObject clickedButton = EventSystem.current.currentSelectedGameObject;

            if (clickedButton == null)
            {
                Debug.LogWarning("No button detected!");
                return;
            }

            // Make sure the button is actually a child of plotButtonParent
            if (clickedButton.transform.parent != plotButtonParent.transform)
            {
                Debug.LogWarning("Clicked button is not under plotButtonParent!");
                return;
            }

            // Get the index of the clicked button among its siblings
            int buttonIndex = clickedButton.transform.GetSiblingIndex();

            GameObject plot = plotParent.transform.GetChild(buttonIndex).gameObject;
            plot.GetComponent<Plot>().SelectThisPlot();
        }

    }

    void GenerateRange()
    {
        if (currentPlotRange != null)
        {
            Destroy(currentPlotRange);
        }

        GameObject plot = plotParent.transform.GetChild(currentPlotSelected).gameObject;

        currentPlotRange = Instantiate(rangePrefab, plot.GetComponent<Plot>().GetTower().transform.position, Quaternion.identity);
        Vector2Int currentTowerTypeAndLevel = plot.GetComponent<Plot>().GetTower().GetComponent<TurretTargeting>().GetTypeAndLevel();

        float currentTowerRange = upgradeManager.getTowerStats()[currentTowerTypeAndLevel.x][currentTowerTypeAndLevel.y - 1][2];

        float scaleMultiplier = plot.GetComponent<Plot>().GetTower().transform.localScale.x * 2f;
        currentPlotRange.transform.localScale = new Vector3(currentTowerRange, currentTowerRange, currentTowerRange) * scaleMultiplier;
    }

    public void SetPlotSelected(int index)
    {
        currentPlotSelected = index;
    }

    public void ShowTowerPanel()
    {
        GenerateRange();

        buyPanel.SetActive(false);
        towerPanel.SetActive(true);
    }

    public void ShowBuyPanel()
    {
        Debug.Log("CALLED");
        Destroy(currentPlotRange);

        buyPanel.SetActive(true);
        towerPanel.SetActive(false);
    }

    public void RefreshTowerPanel()
    {
        if (plotParent.transform.GetChild(currentPlotSelected).GetComponent<Plot>().GetTower() != null)
        {
            Vector2Int towerTypeAndLevel = plotParent.transform.GetChild(currentPlotSelected).GetComponent<Plot>().GetTower().GetComponent<TurretTargeting>().GetTypeAndLevel();
            UpdateTowerPanel(towerTypeAndLevel.x, towerTypeAndLevel.y);
        }
            
    }


    public void UpdateTowerPanel(int towerType, int towerLevel)
    {
        // Tower types
        // 0 = mage
        // 1 = cannon
        // 2 = tesla
        //
        // Levels are given as 1 2 3
        // Upgrades[0] = 1>2, Upgrades[1] = 2>3

        plotTowerIcon.GetComponent<Image>().sprite = upgradeManager.getTowerIcons()[towerType];
        plotNumber.text = "Plot " + currentPlotSelected;
        plotTowerName.text = upgradeManager.getTowerNames()[towerType];
        plotTowerLevel.text = "Level " + towerLevel;

        Debug.Log("LEVEL :  " + towerLevel);
        Debug.Log("TYPE :  " + towerType);

        // Current damage, rate, range
        plotTowerStats[0].text = upgradeManager.getTowerStats()[towerType][towerLevel - 1][0].ToString();
        plotTowerStats[1].text = upgradeManager.getTowerStats()[towerType][towerLevel - 1][1].ToString();
        plotTowerStats[2].text = upgradeManager.getTowerStats()[towerType][towerLevel - 1][2].ToString();

        // Next damage, rate, range
        if (towerLevel >= 3)
        {
            plotTowerNextStats[0].text = "MAX";
            plotTowerNextStats[1].text = "MAX";
            plotTowerNextStats[2].text = "MAX";

            upgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Upgrade: MAX";

        }
        else
        {
            plotTowerNextStats[0].text = upgradeManager.getTowerStats()[towerType][towerLevel][0].ToString();
            plotTowerNextStats[1].text = upgradeManager.getTowerStats()[towerType][towerLevel][1].ToString();
            plotTowerNextStats[2].text = upgradeManager.getTowerStats()[towerType][towerLevel][2].ToString();

            upgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Upgrade: $" + upgradeManager.getTowerUpgradePrices()[towerType][towerLevel - 1];
        }

        sellButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Sell: $" + upgradeManager.getTowerSellPrices()[towerType][towerLevel - 1];
        plotTowerDescription.text = upgradeManager.getTowerDescriptions()[towerType];


        GameObject currentTower = plotParent.transform.GetChild(currentPlotSelected).GetComponent<Plot>().GetTower();

        int money = gameManager.GetCurrency();
        Vector2Int currentTowerTypeAndLevel = currentTower.GetComponent<TurretTargeting>().GetTypeAndLevel();


        if (currentTowerTypeAndLevel.y >= 3)
        {
            upgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.grey;
        }
        else
        {
            int upgradeCost = upgradeManager.getTowerUpgradePrices()[currentTowerTypeAndLevel.x][currentTowerTypeAndLevel.y - 1];

            if (money >= upgradeCost)
            {
                upgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.green;
            }
            else
            {
                upgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }


    }

    public void UpgradeTower()
    {
        GameObject currentTower = plotParent.transform.GetChild(currentPlotSelected).GetComponent<Plot>().GetTower();

        int money = gameManager.GetCurrency();
        Vector2Int currentTowerTypeAndLevel = currentTower.GetComponent<TurretTargeting>().GetTypeAndLevel();

        if (currentTowerTypeAndLevel.y >= 3)
        {
            return;
        }

        int upgradeCost = upgradeManager.getTowerUpgradePrices()[currentTowerTypeAndLevel.x][currentTowerTypeAndLevel.y - 1];



        if (money >= upgradeCost)
        {
            // Upgrade tower
            gameManager.ChangeMoney(-upgradeCost);
            Vector3 newStats = upgradeManager.getTowerStats()[currentTowerTypeAndLevel.x][currentTowerTypeAndLevel.y];

            currentTower.GetComponent<TurretTargeting>().UpgradeTower(newStats);

            UpdateTowerPanel(currentTowerTypeAndLevel.x, currentTowerTypeAndLevel.y + 1);
            GenerateRange();

        }
        else
        {
            // Do nothing
            return;
        }
    }

    public void SellTower()
    {
        Plot currentPlot = plotParent.transform.GetChild(currentPlotSelected).GetComponent<Plot>();
        GameObject currentTower = currentPlot.GetTower();

        Vector2Int currentTowerTypeAndLevel = currentTower.GetComponent<TurretTargeting>().GetTypeAndLevel();
        gameManager.ChangeMoney(upgradeManager.getTowerSellPrices()[currentTowerTypeAndLevel.x][currentTowerTypeAndLevel.y - 1]);

        ShowBuyPanel();

        currentPlot.RemoveTower();

        Destroy(currentPlotRange);
    }
}

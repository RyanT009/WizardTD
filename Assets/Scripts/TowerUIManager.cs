using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUIManager : MonoBehaviour
{
    public static TowerUIManager Instance;

    [SerializeField] private GameObject towerPanel;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button downgradeButton;
    [SerializeField] private Button destroyButton;
    [SerializeField] private Button closeButton;

    private Plot currentSelectedPlot;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Listen for when the buttons are clicked

        Debug.Log("Upgrade button: " + upgradeButton);
        Debug.Log("Downgrade button: " + downgradeButton);
        Debug.Log("Destroy button: " + destroyButton);
        Debug.Log("Close button: " + closeButton);

        upgradeButton.onClick.AddListener(OnUpgradeClicked);
        downgradeButton.onClick.AddListener(OnDowngradeClicked);
        destroyButton.onClick.AddListener(OnDestroyClicked);
        closeButton.onClick.AddListener(OnCloseClicked);
        HideUpgradeUI();
    }

    public void ShowUpgradeUI(Plot plot)
    {
        currentSelectedPlot = plot;

        // Calculate screen position of the current plot
        Vector3 worldPos = currentSelectedPlot.transform.position;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // Offset downwards
        screenPos.y -= 45f;

        // Move the panel
        towerPanel.transform.position = screenPos;

        // Activate the panel
        towerPanel.SetActive(true);
    }

    public void HideUpgradeUI()
    {
        towerPanel.SetActive(false);
        currentSelectedPlot = null;
    }

    void OnUpgradeClicked()
    {
        if (currentSelectedPlot != null)
        {
            currentSelectedPlot.UpgradeTower();
            HideUpgradeUI();
        }
    }
    void OnDowngradeClicked(){
        if (currentSelectedPlot != null){
            currentSelectedPlot.DowngradeTower();
            HideUpgradeUI();
        }
    }

    void OnDestroyClicked()
    {
        if (currentSelectedPlot != null)
        {
            currentSelectedPlot.DestroyTower();
            HideUpgradeUI();
        }
    }

    void OnCloseClicked()
    {
        HideUpgradeUI();
    }
}
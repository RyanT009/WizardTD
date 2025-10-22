using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUIManager : MonoBehaviour
{
    public static TowerUIManager Instance;

    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button destroyButton;

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
        upgradeButton.onClick.AddListener(OnUpgradeClicked);
        destroyButton.onClick.AddListener(OnDestroyClicked);
        HideUpgradeUI();
    }

    public void ShowUpgradeUI(Plot plot)
    {
        currentSelectedPlot = plot;
        upgradePanel.SetActive(true);
    }

    public void HideUpgradeUI()
    {
        upgradePanel.SetActive(false);
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

    void OnDestroyClicked()
    {
        if (currentSelectedPlot != null)
        {
            currentSelectedPlot.DestroyTower();
            HideUpgradeUI();
        }
    }
}
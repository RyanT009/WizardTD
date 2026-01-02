using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] List<string> towerNames;
    [SerializeField] List<string> towerDescriptions;
    [SerializeField] List<Sprite> towerIcons;

    [SerializeField] List<Vector3> towerStats1;
    [SerializeField] List<int> towerUpgradePrices1;
    [SerializeField] List<int> towerSellPrices1;

    [SerializeField] List<Vector3> towerStats2;
    [SerializeField] List<int> towerUpgradePrices2;
    [SerializeField] List<int> towerSellPrices2;

    [SerializeField] List<Vector3> towerStats3;
    [SerializeField] List<int> towerUpgradePrices3;
    [SerializeField] List<int> towerSellPrices3;

    // Combined
    [SerializeField] List<List<Vector3>> allTowerStats = new List<List<Vector3>>();
    [SerializeField] List<List<int>> allTowerUpgradePrices = new List<List<int>>();
    [SerializeField] List<List<int>> allTowerSellPrices = new List<List<int>>();

    void Start()
    {
        allTowerStats.Add(towerStats1);
        allTowerStats.Add(towerStats2);
        allTowerStats.Add(towerStats3);

        allTowerUpgradePrices.Add(towerUpgradePrices1);
        allTowerUpgradePrices.Add(towerUpgradePrices2);
        allTowerUpgradePrices.Add(towerUpgradePrices3);

        allTowerSellPrices.Add(towerSellPrices1);
        allTowerSellPrices.Add(towerSellPrices2);
        allTowerSellPrices.Add(towerSellPrices3);
    }

    public List<string> getTowerNames() { return towerNames; }
    public List<string> getTowerDescriptions() {  return towerDescriptions; }
    public List<Sprite> getTowerIcons() { return towerIcons; }

    public List<List<Vector3>> getTowerStats() { return allTowerStats; }
    public List<List<int>> getTowerUpgradePrices() { return allTowerUpgradePrices; }
    public List<List<int>> getTowerSellPrices() { return allTowerSellPrices; }
}

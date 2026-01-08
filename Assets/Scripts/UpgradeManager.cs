using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] List<TowerUpgrade> upgradeList1 = new List<TowerUpgrade>();
    [SerializeField] List<TowerUpgrade> upgradeList2 = new List<TowerUpgrade>();
    [SerializeField] List<TowerUpgrade> upgradeList3 = new List<TowerUpgrade>();
    [SerializeField] List<TowerUpgrade> upgradeList4 = new List<TowerUpgrade>();
    [SerializeField] List<TowerUpgrade> upgradeList5 = new List<TowerUpgrade>();

    private List<List<TowerUpgrade>> allUpgradeLists = new List<List<TowerUpgrade>>();


    public float enemyHealthMultiplier; // How much health enemy spawns with (1 = 100%)
    public float damageMultiplier; // How much damage each projectile does (1 = 100%)
    public float speedMultiplier; // How fast towers shoot (1 = 100%)

    public float goldMultiplier; // How much money enemies drop (1 = 100%)
    public float priceMultiplier; // How much towers/upgrades cost (1 = 100%)


    void Awake()
    {
        allUpgradeLists.Add(upgradeList1);
        allUpgradeLists.Add(upgradeList2);
        allUpgradeLists.Add(upgradeList3);
        allUpgradeLists.Add(upgradeList4);
        allUpgradeLists.Add(upgradeList5);

        Debug.Log("UPGRADE LIST COUNT:" + allUpgradeLists.Count);
    }

    public List<TowerUpgrade> getUpgradeList(int index)
    {
        if (index > allUpgradeLists.Count || index < 0)
        {
            Debug.Log("GET UPGRADE LIST FAILED WITH INDEX " + index);
            return null;
        }
        return allUpgradeLists[index];
    }

    public void ChangeEnemyHealthMultiplier(float newValue)
    {
        enemyHealthMultiplier = newValue;
    }

    public void ChangeDamageMultiplier(float newValue)
    {
        damageMultiplier = newValue;
    }

    public void ChangeSpeedMultiplier(float newValue)
    {
        speedMultiplier = newValue;
    }

    public void ChangeGoldMultiplier(float newValue)
    {
        goldMultiplier = newValue;
    }

    public void ChangePriceMultiplier(float newValue)
    {
        priceMultiplier = newValue;
    }




}

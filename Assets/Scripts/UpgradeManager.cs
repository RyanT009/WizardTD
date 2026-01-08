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


}

using UnityEngine;

[System.Serializable]
public class TowerUpgrade
{
    public TowerType towerType;

    [Header("Upgrade Info")]
    public string upgradeName;

    [TextArea]
    public string upgradeDescription;

    public int upgradePrice;
    public int sellPrice;
}

public enum TowerType
{
    Archer,
    Fire,
    Cannon,
    Tesla,
    Mage
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currency;
    [SerializeField] int skillPoints;
    [SerializeField] TextMeshProUGUI currencyText;
    [SerializeField] TextMeshProUGUI skillPointsText;

    [SerializeField] UIManager uiManager;


    // Start is called before the first frame update
    void Start()
    {
        currencyText.text = "$" + currency;
        skillPointsText.text = "Skill PTS: " + skillPoints;
        uiManager.UpdateShopUI(currency);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMoney(int amount)
    {
        currency += amount;
        currencyText.text = "$" + currency;

        uiManager.UpdateShopUI(currency);
        uiManager.RefreshTowerPanel();
    }

    public void ChangeSkillPoints(int amount)
    {
        skillPoints += amount;
        skillPointsText.text = "Skill PTS: " + skillPoints;
        uiManager.RefreshSkillTree();
    }

    public int GetCurrency()
    {
        return currency;
    }

    public int GetSkillPoints()
    {
        return skillPoints;
    }
}

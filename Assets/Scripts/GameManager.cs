using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currency;
    [SerializeField] TextMeshProUGUI currencyText;


    // Start is called before the first frame update
    void Start()
    {
        currencyText.text = currency.ToString("N0");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMoney(int amount)
    {
        currency += amount;
        currencyText.text = currency.ToString("N0");
    }

    public int GetCurrency()
    {
        return currency;
    }
}

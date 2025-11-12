using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currency;
    [SerializeField] TextMeshProUGUI currencyText;


    // Start is called before the first frame update
    void Start()
    {
        currency = 250;
        updateCurrency();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void updateCurrency()
    {
        currencyText.text = "Gold: " + currency;
    }
}

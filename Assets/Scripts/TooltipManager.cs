using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{

    [SerializeField] List<RoundTooltip> roundTooltips = new List<RoundTooltip>();
    [SerializeField] TextMeshProUGUI tooltipText;
    [SerializeField] GameObject popup;
    [SerializeField] float popupSpeed;
    private int relativeMovement = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckForTooltips(int waveNumber)
    {
        foreach (RoundTooltip tooltip in roundTooltips)
        {
            if (tooltip.roundNumber == waveNumber)
            {
                ShowToolTip(tooltip.tooltip);
                return;
            }
        }

        Debug.Log("No tooltips for this round");
    }

    void ShowToolTip(string text)
    {
        tooltipText.text = text;
        OpenPopUp();
    }

    void OpenPopUp()
    {
        InvokeRepeating("MoveUp", 0f, popupSpeed);
    }

    void MoveUp()
    {
        popup.transform.position = new Vector2(popup.transform.position.x, popup.transform.position.y + 6);
        relativeMovement += 6;

        if (relativeMovement >= 240)
        {
            relativeMovement = 240;
            CancelInvoke("MoveUp");
        }
    }

    public void ClosePopUp()
    {
        CancelInvoke("MoveUp");
        InvokeRepeating("MoveDown", 0f, popupSpeed);
    }

    void MoveDown()
    {
        popup.transform.position = new Vector2(popup.transform.position.x, popup.transform.position.y - 6);
        relativeMovement -= 6;

        if (relativeMovement <= 0)
        {
            relativeMovement = 0;
            CancelInvoke("MoveDown");
        }
    }
}

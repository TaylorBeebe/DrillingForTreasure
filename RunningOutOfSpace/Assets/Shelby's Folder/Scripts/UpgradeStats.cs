using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeStats : MonoBehaviour, IPointerDownHandler
{

    public string upgradeName;
    public string upgradeDescription;
    public int upgradePrice;
    
    Text upgradeNameText;
    Text upgradeDescriptionText;
    Text upgradePriceText;

    int upgradeLevel;

    void Start()
    {
        upgradeNameText = GameObject.Find("UpgradeName").GetComponent<Text>();
        upgradeDescriptionText = GameObject.Find("UpgradeDescription").GetComponent<Text>();
        upgradePriceText = GameObject.Find("UpgradeCost").GetComponent<Text>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        upgradeNameText.text = upgradeName;
        upgradeDescriptionText.text = upgradeDescription;
        upgradePriceText.text = "LEVEL " + upgradeLevel + " AVAILABLE FOR      " + upgradePrice;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeStats : MonoBehaviour, IPointerDownHandler
{

    public enum UpgradeType
    {
        Gameplay,
        Premium

    }

    public UpgradeType upgradeType;
    public string upgradeName;
    public string upgradeDescription;
    public int upgradePrice;
    int basePrice;

    Text upgradeNameText;
    Text upgradeDescriptionText;
    Text upgradePriceText;
    Text upgradeLevelText;

    public int upgradeLevel = 0;

    public GameObject upgradeButton;

    void Start()
    {
        basePrice = upgradePrice;
        upgradeButton = GameObject.Find("UpgradeButton");
        upgradeNameText = GameObject.Find("UpgradeName").GetComponent<Text>();
        upgradeDescriptionText = GameObject.Find("UpgradeDescription").GetComponent<Text>();
        upgradePriceText = GameObject.Find("UpgradeCost").GetComponent<Text>();
        upgradeLevelText = transform.Find("LevelText").GetComponent<Text>();
    }

    void Update()
    {
        upgradeLevelText.text = "" + upgradeLevel;
        upgradePrice = (upgradeLevel + 1) * basePrice;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        upgradeNameText.text = upgradeName;
        upgradeDescriptionText.text = upgradeDescription;


        if (upgradeType == UpgradeType.Gameplay)
        {
            if (upgradeLevel > 2)
            {
                upgradeButton.SetActive(false);
                upgradePriceText.text = "MAX LEVEL";
            }
            else
            {
                upgradeButton.SetActive(true);
                upgradePriceText.text = "LEVEL " + (upgradeLevel + 1) + " AVAILABLE FOR " + upgradePrice + " SCRAP";
            }
        }
        else if (upgradeType == UpgradeType.Premium)
        {
            if (upgradeLevel > 0)
            {
                upgradeButton.SetActive(false);
                upgradePriceText.text = "MAX LEVEL";
            }
            else
            {
                upgradeButton.SetActive(true);
                upgradePriceText.text = "AVAILABLE FOR " + upgradePrice + " GEMS";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeMenuManager : MonoBehaviour
{
    bool inUpgradedShop = false;

    GameObject GameplayUpgradeScreen;
    GameObject PremiumUpgradeScreen;

    void Start()
    {
        GameplayUpgradeScreen = GameObject.Find("GameplayUpgradeScreen");
        PremiumUpgradeScreen = GameObject.Find("PremiumStoreScreen");

        PremiumUpgradeScreen.SetActive(false);
    }

    public void SwapShops()
    {
        inUpgradedShop = !inUpgradedShop;

        GameObject.Find("UpgradeName").GetComponent<Text>().text = "-";
        GameObject.Find("UpgradeDescription").GetComponent<Text>().text = "-";


        if (!inUpgradedShop)
            GameObject.Find("UpgradeCost").GetComponent<Text>().text = "LEVEL -" + " AVAILABLE FOR      -";
        else
            GameObject.Find("UpgradeCost").GetComponent<Text>().text = "AVAILABLE FOR      -";

        if (inUpgradedShop)
        {
            GameplayUpgradeScreen.SetActive(false);
            PremiumUpgradeScreen.SetActive(true);
        }
        else
        {
            GameplayUpgradeScreen.SetActive(true);
            PremiumUpgradeScreen.SetActive(false);
        }

    }
}

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

        if(inUpgradedShop)
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

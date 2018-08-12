using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Sprite[] upgradeSprites;
    //1 = Normal
    //2 = Hover
    //3 = Unavailable

    public bool upgradeUnavailable = false;

    void Update()
    {
        if (upgradeUnavailable)
        {
            GetComponent<Image>().sprite = upgradeSprites[2];
            GetComponent<Button>().enabled = false;  
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!upgradeUnavailable)
            GetComponent<Image>().sprite = upgradeSprites[0];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {       
        if (!upgradeUnavailable)
            GetComponent<Image>().sprite = upgradeSprites[1];
    }
}

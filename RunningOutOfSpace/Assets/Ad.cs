using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ad : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{

    [System.Serializable]
    public enum AdType
    {
        Closable,
        Test,
        Test2,
        Test3
    }

    public enum AdSize
    {
        Box,
        Horizontal,
        Vertical,
        VerticalSmall
    }

    Sprite[] boxAds;
    Sprite[] horizontalAds;
    Sprite[] verticalAds;
    Sprite[] verticalSmallAds;

    public AdType adType;
    public AdSize adSize;

    Vector3 tempPos;

    void Start()
    {
        //Grab the sprites from the Canvas
        boxAds = transform.parent.GetComponent<AdSpawner>().boxAds;
        horizontalAds = transform.parent.GetComponent<AdSpawner>().horizontalAds;
        verticalAds = transform.parent.GetComponent<AdSpawner>().verticalAds;
        verticalSmallAds = transform.parent.GetComponent<AdSpawner>().verticalSmallAds;

        //Check what ad size the prefab is
        if (adSize == AdSize.Box)
            GetComponent<Image>().sprite = boxAds[Random.Range(0, boxAds.Length)];
        if (adSize == AdSize.Horizontal)
            GetComponent<Image>().sprite = horizontalAds[Random.Range(0, horizontalAds.Length)];
        if (adSize == AdSize.Vertical)
            GetComponent<Image>().sprite = verticalAds[Random.Range(0, verticalAds.Length)];
        if (adSize == AdSize.VerticalSmall)
            GetComponent<Image>().sprite = verticalAds[Random.Range(0, verticalSmallAds.Length)];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Move the Ad to the front and get a temp pos of the mouse
        tempPos = Input.mousePosition - transform.position;
        transform.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        //Set the ad position to the mouse position and subtract the offset
        transform.position = Input.mousePosition - tempPos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Nothing
    }

    public void CloseAd()
    {
        //Destroy the Ad and play the closing animation
        Destroy(gameObject, 0.15f);
        GetComponent<Animator>().SetTrigger("AdClosed");
    }
}

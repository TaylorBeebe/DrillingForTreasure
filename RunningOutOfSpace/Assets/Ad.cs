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
        Timer,
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

    //Button settings
    public Button closeButton;

    //Timer ad settings
    float adTimer;
    float adTimerStart;
    public Image timer;
    public Text timerText;

    void Start()
    {
        //Set references
        closeButton = transform.FindChild("ExitButton").GetComponent<Button>();

        if (adType == AdType.Timer)
        {
            timer = transform.FindChild("Timer").GetComponent<Image>();
            timerText = timer.transform.FindChild("TimeText").GetComponent<Text>();               
        }

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
            GetComponent<Image>().sprite = verticalSmallAds[Random.Range(0, verticalSmallAds.Length)];

        //Check the ad type
        if (adType == AdType.Timer)
        {
            closeButton.gameObject.SetActive(false);

            float minTime = transform.parent.GetComponent<AdSpawner>().timerAdMin;
            float maxTime = transform.parent.GetComponent<AdSpawner>().timerAdMax;

            adTimer = Random.Range(minTime, maxTime);
            adTimerStart = adTimer;          
        }
    }
    void Update()
    {
        if(adType == AdType.Timer)
        {
            adTimer -= Time.deltaTime;
            timer.fillAmount = adTimer / adTimerStart;
            timerText.text = Mathf.Floor(adTimer).ToString();
            if (adTimer <= 0)
            {
                timer.gameObject.SetActive(false);
                closeButton.gameObject.SetActive(true);
            }
        }
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

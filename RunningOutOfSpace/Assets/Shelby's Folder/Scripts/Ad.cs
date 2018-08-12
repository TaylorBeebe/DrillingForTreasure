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
        StickyKeys,
        CaptchaClose,
        CaptchaType
    }

    public enum AdSize
    {
        Box,
        Horizontal,
        Vertical,
        VerticalSmall,
        StickyKeys
    }

    Sprite[] boxAds;
    Sprite[] horizontalAds;
    Sprite[] verticalAds;
    Sprite[] verticalSmallAds;
    Sprite[] stickyKeysAds;

    public AdType adType;
    public AdSize adSize;

    Vector3 tempPos;

    //[Header("Ad sprites")]
    Button closeButton;

    float adTimer;
    float adTimerStart;
    Image timer;
    Text timerText;

    string captchaText;
    Text captchaInput;

    void Start()
    {
        //Grab the sprites from the Canvas
        boxAds = transform.parent.GetComponent<AdSpawner>().boxAds;
        horizontalAds = transform.parent.GetComponent<AdSpawner>().horizontalAds;
        verticalAds = transform.parent.GetComponent<AdSpawner>().verticalAds;
        verticalSmallAds = transform.parent.GetComponent<AdSpawner>().verticalSmallAds;
        stickyKeysAds = transform.parent.GetComponent<AdSpawner>().stickyKeysAds;

        //Check if the ad needs a special sprite
        if (adType == AdType.StickyKeys)
            GetComponent<Image>().sprite = stickyKeysAds[Random.Range(0, stickyKeysAds.Length)];

        if (adType == AdType.Timer || adType == AdType.Closable)
        {
            closeButton = transform.Find("ExitButton").GetComponent<Button>();

            //Check what ad size the prefab is
            if (adSize == AdSize.Box)
                GetComponent<Image>().sprite = boxAds[Random.Range(0, boxAds.Length)];
            if (adSize == AdSize.Horizontal)
                GetComponent<Image>().sprite = horizontalAds[Random.Range(0, horizontalAds.Length)];
            if (adSize == AdSize.Vertical)
                GetComponent<Image>().sprite = verticalAds[Random.Range(0, verticalAds.Length)];
            if (adSize == AdSize.VerticalSmall)
                GetComponent<Image>().sprite = verticalSmallAds[Random.Range(0, verticalSmallAds.Length)];
        }

        //Check the ad type
        if (adType == AdType.Timer)
        {
            timer = transform.Find("Timer").GetComponent<Image>();
            timerText = timer.transform.Find("TimeText").GetComponent<Text>();

            closeButton.gameObject.SetActive(false);

            float minTime = transform.parent.GetComponent<AdSpawner>().timerAdMin;
            float maxTime = transform.parent.GetComponent<AdSpawner>().timerAdMax;

            adTimer = Random.Range(minTime, maxTime);
            adTimerStart = adTimer;          
        }

        if(adType == AdType.CaptchaType)
        {
            captchaText = transform.parent.GetComponent<AdSpawner>().captchaTypeText;
            captchaInput = transform.Find("CaptchaText").transform.Find("CaptcaTextInput").GetComponent<Text>();
        }
    }
    void Update()
    {
        //Timer ad gameplay
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

        if (adType == AdType.CaptchaType)
        {
            if(transform.Find("CaptchaText").GetComponent<InputField>().text == captchaText)
            {
                CloseAd();
                captchaInput.transform.parent.gameObject.SetActive(false);
                GetComponent<Ad>().enabled = false;
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
        GetComponent<Animator>().SetTrigger("AdClosed");
        Destroy(gameObject, 0.15f);       
    }

    public void spawnAds(int ads)
    {
        transform.parent.GetComponent<AdSpawner>().SpawnAdCustom(ads);
        Destroy(gameObject);
    }
}

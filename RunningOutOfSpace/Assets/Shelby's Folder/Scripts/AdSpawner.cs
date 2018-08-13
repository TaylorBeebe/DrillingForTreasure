using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdSpawner : MonoBehaviour {

    [Header("Ad prefabs")]
    public GameObject[] ads;
    public GameObject[] stickyKeys;

    [Header("Spawn settings")]
    public float adSpawnDeley;
    public float adSpawnWindowMin;
    public float adSpawnWindowMax;
    public bool stopSpawning;

    [Header("Timer ad settings")]
    public float timerAdMin;
    public float timerAdMax;

    [Header("Sticky Keys settings")]
    public int shiftsToActivate;
    public float shiftResetTime;
    int stickyPress;

    [Header("Captcha Settings")]
    public string captchaTypeText;

    [Header("Ad sprites")]
    public Sprite[] boxAds;
    public Sprite[] horizontalAds;
    public Sprite[] verticalAds;
    public Sprite[] verticalSmallAds;
    public Sprite[] stickyKeysAds;

    void Start()
    {
        //StopSpawn(5f);
        if(!stopSpawning)
            Invoke("SpawnAds", adSpawnDeley);       
    }

    void Update()
    {
        Invoke("resetSticky", shiftResetTime);
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            stickyPress++;
            CancelInvoke("resetSticky");
        }
        if (stickyPress >= shiftsToActivate)
        {
            stickyPress = 0;
            Instantiate(stickyKeys[0], new Vector3(Screen.width/2,Screen.height/2,0), Quaternion.identity, transform);
        }
    }

    void resetSticky()
    {
        stickyPress = 0;
    }

    public void SpawnAds()
    {
        //Choose a random ad
        int randomAd = Random.Range(0, ads.Length);

        //Get a random position on the screen
        float xPos = Random.Range(0, Screen.width);
        float yPos = Random.Range(0, Screen.height);
        Vector3 spawnPoint = new Vector3(xPos, yPos, 0);

        //Spawn random ad to a random position
        Instantiate(ads[randomAd], spawnPoint, Quaternion.identity, transform);

        //Spawn the next ad if stopSpawning is false
        float randomSpawnNum = Random.Range(adSpawnWindowMin, adSpawnWindowMax);

        if(!stopSpawning)
            Invoke("SpawnAds", randomSpawnNum);
    }

    public void SpawnAdCustom(int n)
    {
        for (int i = 0; i < n; i++)
        {
            //Choose a random ad
            int randomAd = Random.Range(0, ads.Length);

            //Get a random position on the screen
            float xPos = Random.Range(0, Screen.width);
            float yPos = Random.Range(0, Screen.height);
            Vector3 spawnPoint = new Vector3(xPos, yPos, 0);

            //Spawn random ad to a random position
            Instantiate(ads[randomAd], spawnPoint, Quaternion.identity, transform);
        }
    }

    public void StopSpawn(float time)
    {
        stopSpawning = true;
        Invoke("StartSpawn", time);
    }

    public void StartSpawn()
    {
        stopSpawning = false;
        Invoke("SpawnAd", 0);
    }
}

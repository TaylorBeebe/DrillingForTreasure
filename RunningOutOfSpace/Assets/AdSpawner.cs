using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdSpawner : MonoBehaviour {

    [Header("Ad prefabs")]
    public GameObject[] ads;

    [Header("Spawn settings")]
    public float adSpawnDeley;
    public float adSpawnWindowMin;
    public float adSpawnWindowMax;

    [Header("Ad sprites")]
    public Sprite[] boxAds;
    public Sprite[] horizontalAds;
    public Sprite[] verticalAds;
    public Sprite[] verticalSmallAds;

    void Start()
    {
        Invoke("SpawnAd", adSpawnDeley);
    }

    void SpawnAd()
    {
        int randomAd = Random.Range(0, ads.Length);

        float xPos = Random.Range(0, Screen.width);
        float yPos = Random.Range(0, Screen.height);

        Vector3 spawnPoint = new Vector3(xPos, yPos, 0);

        Instantiate(ads[randomAd], spawnPoint, Quaternion.identity, transform);


        float randomSpawnNum = Random.Range(adSpawnWindowMin, adSpawnWindowMax);
        Invoke("SpawnAd", randomSpawnNum);
    }
}

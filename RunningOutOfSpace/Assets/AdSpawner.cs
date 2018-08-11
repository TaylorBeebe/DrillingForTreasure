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
    public bool stopSpawning;

    [Header("Ad sprites")]
    public Sprite[] boxAds;
    public Sprite[] horizontalAds;
    public Sprite[] verticalAds;
    public Sprite[] verticalSmallAds;

    void Start()
    {
        //StopSpawn(5f);
        Invoke("SpawnAd", adSpawnDeley);       
    }

    void SpawnAd()
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
            Invoke("SpawnAd", randomSpawnNum);
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

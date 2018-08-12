using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    //Array holding the different enemy types
    [SerializeField] GameObject Enemy1, Enemy2, Enemy3;

    //Get and create set for array so it can be initialized at start
    public GameObject[] Enemyarray
    {
        get;
        private set;
    }

    //Current state of spawning
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    };

    /*Wave class currently elementary
    Wave class will eventually be used to calculate random waves based
    on level number
    */
    [System.Serializable]
    protected class Wave
    {
        private Stack<string> waveMakeup;
        private int count;
        private Vector2 spawnLocation;
        private int rate = 2;
        private bool doesGathererSpawn;

        public Stack<string> GetWave()
        {
            return waveMakeup;
        }
        public void SetWave(Stack<string> wave)
        {
            waveMakeup = wave;
        }
        public int GetCount()
        {
            return count;
        }
        public void SetCount(int newCount)
        {
            count = newCount;
        }
        public Vector2 GetSpawnLocation()
        {
            return spawnLocation;
        }
        public void SetSpawnLocation(Vector2 newSpawnLocation)
        {
            spawnLocation = newSpawnLocation;
        }
        public int GetRate()
        {
            return rate;
        }
        public void SetRate(int newRate)
        {
            rate = newRate;
        }
        public bool GetDoesGathererSpawn()
        {
            //roll dice here. super small chance
            return doesGathererSpawn;
        }
    }

    //wait time for checking if any enemies are alive
    public float delaySearchEnemies = 2f;

    //predetermined time between waves. Will eventually be a function of level
    private float timeBetweenWaves = 5f;

    //time until next wave spawn
    public float waveCountdown;

    //get main camera
    public Camera mainCamera;

    //initialize state to SPAWNING
    public SpawnState state = SpawnState.COUNTING;

    //holds aspect ration
    private float screenAspect;

    //holds camera height/2
    private float cameraHeight;

    //holds camera width/2
    private float cameraWidth;


    /* @ Param: None
     * @ Pre: None
     * @ Post: Initialize Variables
     */
    void Start()
    {
        //initialize the wave countdown max
        waveCountdown = timeBetweenWaves;

        //initialize array
        Enemyarray = new GameObject[] {
            Enemy1, Enemy2, Enemy3
        };

        screenAspect = (float)Screen.width / (float)Screen.height;
        cameraHeight = mainCamera.orthographicSize;
        cameraWidth = screenAspect * cameraHeight;
    }

    /* @ Param: None
     * @ Pre: None
     * @ Post: Update run every frame
     */
    void Update()
    {

        if (state == SpawnState.COUNTING)
        {
            waveCountdown -= Time.deltaTime;
        }
        if (waveCountdown <= 0)
        {
            //If we are already spawning, we don't want to start a new coroutine for spawning
            if (state != SpawnState.SPAWNING)
            {
                Wave newWave = new Wave();
                Debug.Log("State != Spawning");
                //Coroutine spawns waves so they don't need to be spawned all at one time
                StartCoroutine(SpawnWave(newWave));
            }
        }
    }

    /* @ Param: Wave of type game object
     * @ Pre: Enemies not currently spawning
     * @ Post: Enemies in wave spawned
     */
    IEnumerator SpawnWave(Wave wave)
    {

        //start spawning waves. Set state so more waves aren't spawned simultaneously
        //this will be replaced with wave spawning counter, allowing waves to spawn concurrently
        //as a function of level
        state = SpawnState.SPAWNING;

        //increment through elements in wave
        for (int i = 0; i < wave.GetCount(); i++)
        {
            Vector2 spawnLocation = GetRandomSpawnLocation();
            SpawnEnemy(spawnLocation);
            yield return new WaitForSeconds(1f / wave.GetRate());
        }

        state = SpawnState.COUNTING;

        Debug.Log("SpawnState = Counting");

        //reset wave countdown
        waveCountdown = timeBetweenWaves;

        yield break;
    }


    /* @ Param: Spawn location enemy will be spawned at
     * @ Pre: None
     * @ Post: Enemy spawned at location
     */
    void SpawnEnemy(Vector2 spawnLocation)
    {

        //Debug.Log("Spawning Enemies at " + spawnLocation);
        //Pick an enemy to spawn randomly
        int enemyToSpawn = Random.Range(0, 3);
        //Instantiate the enemy at the spawn location passed in
        Instantiate(Enemyarray[enemyToSpawn], spawnLocation, Quaternion.identity);
    }

    /* @ Param: None
     * @ Pre: None
     * @ Post: Returns bool true if an enemy is alive
     */
    bool AreEnemiesAlive()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            //enemy with tag found
            return false;
        }
        //return true if no enemy with tag is found
        return true;
    }

    /* @ Param: None
     * @ Pre: Enemies are spawning
     * @ Post: Returns vector2 location where enemy(s) will spawn
     */
    Vector2 GetRandomSpawnLocation()
    {

        //Either the width or the height values need to be maxed to keep the spawning outside screen view
        int whichNumberToMax = Random.Range(0, 2);
        int[] randomizeMaxMin = { -1, 1 };
        int randomizedValue = randomizeMaxMin[Random.Range(0, 2)];
        Vector2 returnValue;

        //If it's zero, max out the camera width
        if (whichNumberToMax == 0)
        {
            //Debug.Log("returning returning maxed width");
            returnValue = new Vector2(((mainCamera.transform.position.x + cameraWidth) * randomizedValue) + randomizedValue, mainCamera.transform.position.y + Random.Range(cameraHeight * -1, cameraHeight));

        }
        //otherwise, max out camera height value
        else
        {
            //Debug.Log("returning maxed height");
            returnValue = new Vector2(mainCamera.transform.position.x + Random.Range(cameraWidth * (-1), cameraWidth), ((mainCamera.transform.position.y + cameraHeight) * randomizedValue) + randomizedValue);

        }
        //Debug.Log("Returning: " + returnValue);
        return returnValue;
    }

    /* @ Param: Current level player is on
     * @ Pre: Wave is being generated
     * @ Post: Returns a stack of names of the enemies to be spawned
     */
    Stack<string> GenerateWaveMakeup(int count)
    {

        Stack<string> wave = new Stack<string>();



        return wave;
    }
}

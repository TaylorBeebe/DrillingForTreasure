using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    
    //Array holding the different enemy types
    [SerializeField] GameObject Enemy1, Enemy2, Enemy3;

    //get and create set for array so it can be initialized at start
    public GameObject[] Enemyarray {
        get;
        private set;
    }

    //Holds current state of spawning
    public enum SpawnState {
        SPAWNING,
        WAITING,
        COUNTING
    };

    

    //Wave class currently elementary
    //Wave class will eventually be used to calculate random waves based
    //on level number
    [System.Serializable]
    public class Wave {
        public string name;
        public int count;
        public float rate;
        public Transform location;   
    }

    //Holds waves. Will need to be generated
    public Wave[] waves;

    //current index of wave
    private int nextWave = 0;

    //wait time for checking if any enemies are alive
    public float delaySearchEnemies = 2f;

    //predetermined time between waves. Will eventually be a function of level
    public float timeBetweenWaves = 5f;

    //time til next wave spawn
    public float waveCountdown;

    //bool holds state of level
    public bool levelOver;

    //get main camera
    public Camera mainCamera;

    //initialize state to SPAWNING
    public SpawnState state = SpawnState.COUNTING;

    //holds aspect ration
    private float screenAspect;

    //gets camera height/2
    private float cameraHeight;

    //gets camera width/2
    private float cameraWidth;

    /* @ Param: None
     * @ Pre: None
     * @ Post: Initialize Variables
     */
    void Start() {
        //initialize the wave countdown max
        waveCountdown = timeBetweenWaves;

        //initialize array
        Enemyarray = new GameObject[] {
            Enemy1, Enemy2, Enemy3
        };
        
        Debug.Log(mainCamera.orthographicSize);
        screenAspect = (float)Screen.width / (float)Screen.height;
        cameraHeight = mainCamera.orthographicSize;
        cameraWidth = screenAspect * cameraHeight;
    Debug.Log(cameraWidth + ", " + cameraHeight);
    }

    /* @ Param: None
     * @ Pre: None
     * @ Post: Update run every frame
     */
    void Update() {

        //delaySearchEnemies -= Time.deltaTime;

        if (state == SpawnState.WAITING)
        {
            /*
            //check if enemies are alive?
            if (delaySearchEnemies <= 0) {
                if (AreEnemiesAlive())
                {
                    //check if there are more enemies to spawn?
                }
                delaySearchEnemies = 2f;
            }
            */
        }
        else if (state == SpawnState.COUNTING) {
            waveCountdown -= Time.deltaTime;
        }
        if (waveCountdown <= 0) {
            //If we are already spawning, we don't want to start a new coroutine for spawning
            if (state != SpawnState.SPAWNING)
            {
                Debug.Log("State != Spawning");
                //Coroutine spawns waves so they don't need to be spawned all at one time
                StartCoroutine(SpawnWave(waves[nextWave]));
                nextWave += 1;
            }
        }
    }

    /* @ Param: Wave of type game object
     * @ Pre: Enemies not currently spawning
     * @ Post: Enemies in wave spawned
     */
    IEnumerator SpawnWave(Wave wave) {
        
        //start spawning waves. Set state so more waves aren't spawned simultaneously
        //this will be replaced with wave spawning counter, allowing waves to spawn concurrently
        //as a function of level
        state = SpawnState.SPAWNING;

        //increment through elements in wave
        for (int i = 0; i < wave.count; i++) {
            SpawnEnemy(wave.location);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        Debug.Log("wave length: " + waves.Length);
        if (nextWave == waves.Length)
        {
            state = SpawnState.WAITING;
            Debug.Log("SpawnState = Waiting");
        }
        else {
            state = SpawnState.COUNTING;
            Debug.Log("SpawnState = Counting");
        }
        waveCountdown = timeBetweenWaves;

        yield break;
    }

    /* @ Param: Spawn location enemy will be spawned at
     * @ Pre: None
     * @ Post: Enemy spawned at location
     */
    void SpawnEnemy(Transform spawnLocation) {
        Debug.Log("Spawning Enemies at " + spawnLocation.position);
        Vector2 spawn = GetRandomSpawnLocation(cameraWidth, cameraHeight);
        Instantiate(Enemy1, spawn, Quaternion.identity);
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

    /* @ Param:
     * @ Pre: 
     * @ Post: 
     */
    Vector2 GetRandomSpawnLocation(float cameraWidth, float cameraHeight) {
        return new Vector2(mainCamera.transform.position.x + cameraWidth, mainCamera.transform.position.y + cameraHeight);
    }

}

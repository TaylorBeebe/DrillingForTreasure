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
    //[System.Serializable]
    protected class Wave
    {
        private Stack<string> waveMakeup;
        private int count;
        private Vector2 spawnLocation;
        public int rate = 2;
        private bool doesGathererSpawn;
        
        private List<string> easyEnemiesIntroduced = new List<string>();
        private List<string> mediumEnemiesIntroduced = new List<string>();
        private List<string> hardEnemiesIntroduced = new List<string>();

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
        public List<string> GetEasyEnemiesAvailableForWaves() {
            return easyEnemiesIntroduced;
        }
        public void AddEasyEnemiesAvailableForWaves(string newEasyEnemy) {
            easyEnemiesIntroduced.Add(newEasyEnemy);
        }
        public List<string> GetMediumEnemiesAvailableForWaves()
        {
            return mediumEnemiesIntroduced;
        }
        public void AddMediumEnemiesAvailableForWaves(string newEasyEnemy)
        {
            mediumEnemiesIntroduced.Add(newEasyEnemy);
        }
        public List<string> GetHardEnemiesAvailableForWaves()
        {
            return hardEnemiesIntroduced;
        }
        public void AddHardEnemiesAvailableForWaves(string newEasyEnemy)
        {
            hardEnemiesIntroduced.Add(newEasyEnemy);
        }
        public string GetEnemy(string enemyType)
        {
            string enemyGenerated;

            if (enemyType == "medium")
            {
                enemyGenerated = GetRandomMediumEnemy();
            }
            else if (enemyType == "hard")
            {
                enemyGenerated = GetRandomHardEnemy();
            }
            else
            {
                enemyGenerated = GetRandomEasyEnemy();
            }

            //Debug.Log("returning enemy: " + enemyGenerated);

            return enemyGenerated;
        }
        public string GetRandomEasyEnemy()
        {
            int random = Random.Range(0, easyEnemiesIntroduced.Count);

            //Debug.Log(random);

            return easyEnemiesIntroduced[random];
        }
        public string GetRandomMediumEnemy()
        {
            int random = Random.Range(0, mediumEnemiesIntroduced.Count);

            return mediumEnemiesIntroduced[random];
        }
        public string GetRandomHardEnemy()
        {
            int random = Random.Range(0, hardEnemiesIntroduced.Count);
           
            return hardEnemiesIntroduced[random];
        }
    }

    //Level we are currently on (will be gotten from GameMode Script)
    public int levelNumber;

    //wait time for checking if any enemies are alive
    public float delaySearchEnemies = 2f;

    //predetermined time between waves. Will eventually be a function of level
    private float timeBetweenWaves;

    //time until next wave spawn
    public float waveCountdown;

    //number of enemies that will appear this wave
    private float enemiesPerWave;

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

    //didn't account for first few levels because all enemies will be easy
    private int roundStartCalculatingEasyPercentage = 3; 

    //Round when the first medium enemy spawns
    private int roundMediumEnemiesStartSpawning = 3;

    //Round when the first hard enemy spawns
    private int roundHardEnemiesStartSpawning = 7;

    //Round after which easy enemies head infinitely toward their final percentage (10%)
    private int roundEasyEnemiesGoToInfinity = 9;

    //Round after which medium enemies head infinitely toward their final percentage (40%)
    private int roundMediumEnemiesGoToInfinity = 9;

    //Round after which hard enemies head infinitely toward their final percentage (50%)
    private int roundHardEnemisGoToInfinity = 9;

    //Easy enemies in game
    private string[] easyEnemies = {"mites"};

    //Medium enemies in game
    private string[] mediumEnemies = {"exploders", "spitters"};

    //Hard enemies in game
    private string[] hardEnemies = { "clammies", "demolishers" };

    //FOR TESTING
    private Stack<string> enemyIntroductionOrder;

    private Wave waveForLevel;

    /* @ Param: None
     * @ Pre: None
     * @ Post: Initialize Variables
     */
    void Start()
    {

        levelNumber = 1;

        //timeBetweenWaves = CalculateTimeBetweenWaves();

        timeBetweenWaves = 1f;
        
        //initialize the wave countdown max
        waveCountdown = timeBetweenWaves;

        enemiesPerWave = CalculateEnemiesPerWave();

        waveForLevel = new Wave();

        enemyIntroductionOrder = new Stack<string>();
        
        shuffle(easyEnemies);
        shuffle(mediumEnemies);
        shuffle(hardEnemies);
        
        for (int x = 0; x < hardEnemies.Length; x++)
        {
            enemyIntroductionOrder.Push(hardEnemies[x]);
        }
        for (int x = 0; x < mediumEnemies.Length; x++)
        {
            enemyIntroductionOrder.Push(mediumEnemies[x]);
        }
        for (int x = 0; x < easyEnemies.Length; x++)
        {
            enemyIntroductionOrder.Push(easyEnemies[x]);
        }

        waveForLevel.AddEasyEnemiesAvailableForWaves(enemyIntroductionOrder.Pop());
        
        //initialize array of enemies
        Enemyarray = new GameObject[] {
            Enemy1, Enemy2, Enemy3
        };

        screenAspect = (float)Screen.width / (float)Screen.height;
        cameraHeight = mainCamera.orthographicSize;
        cameraWidth = screenAspect * cameraHeight;

       

        //Code for testing totals of the formulas added together
        /*
        for (int x = 1; x < 9; x++) {
            Debug.Log("X = " + x + ": " + System.Math.Round((PercentageEasyEnemiesBeforeInfinity(x) + PercentageHardEnemiesBeforeInfinity(x) + PercentageMediumEnemiesBeforeInfinity(x)),2));

        }
        for (int x = 9; x < 21; x++) {
            Debug.Log("X = " + x + ": " + System.Math.Round(((PercentageEasyEnemiesAtInfinity(x) + PercentageHardEnemiesAtInfinity(x) + PercentageMediumEnemiesAtInfinity(x))), 2));
        }
        */

        //Code for testing time between waves as a function of level
        /*
        for (int x = 1; x < 21; x++) {
            Debug.Log("X = " + x + ", Enemies Per Wave: " + CalculateEnemiesPerWave(x));
        }
        for (int x = 1; x < 21; x++)
        {
            Debug.Log("X = " + x + ", TimeBetweenWaves: " + CalculateTimeBetweenWaves(x));
        }
        */
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
                //Coroutine spawns waves so they don't need to be spawned all at one time
                StartCoroutine(SpawnWave(waveForLevel));
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

        Stack<string> waveMakeup = GenerateWaveMakeup(wave);

        state = SpawnState.SPAWNING;
        //Debug.Log("State = Spawning");
        //increment through elements in wave

        int indexLen = waveMakeup.Count;

        

        for (int i = 0; i < indexLen; i++)
        {
            //Debug.Log("Index: " + i);
            Vector2 spawnLocation = GetRandomSpawnLocation();
            string enemy = waveMakeup.Pop();
            //Debug.Log("Count: " + waveMakeup.Count);
            //Debug.Log("Popping: " + enemy);
            
            SpawnEnemy(enemy, spawnLocation);
            yield return new WaitForSeconds(1f / wave.GetRate());
            
        }

        //FOR TESTING COMMENT OUT LATER
        ///*
        levelNumber++;
        
        if (levelNumber % 2 == 1 && levelNumber <= 9) {

            string enemyToAdd = enemyIntroductionOrder.Pop();

            Debug.Log("Adding Enemy: " + enemyToAdd);

            if (IsPresent(easyEnemies, enemyToAdd)) {
                waveForLevel.AddEasyEnemiesAvailableForWaves(enemyToAdd);
            }
            if (IsPresent(mediumEnemies, enemyToAdd))
            {
                waveForLevel.AddMediumEnemiesAvailableForWaves(enemyToAdd);
            }
            if (IsPresent(hardEnemies, enemyToAdd))
            {
                waveForLevel.AddHardEnemiesAvailableForWaves(enemyToAdd);
            }
        }

        //timeBetweenWaves = CalculateTimeBetweenWaves();
        
        timeBetweenWaves = 1f;
        
        //initialize the wave countdown max
        waveCountdown = timeBetweenWaves;

        enemiesPerWave = CalculateEnemiesPerWave();
        //*/
        state = SpawnState.COUNTING;

        //Debug.Log("SpawnState = Counting");

        //reset wave countdown
        waveCountdown = timeBetweenWaves;

        yield break;
    }


    /* @ Param: Spawn location enemy will be spawned at
     * @ Pre: None
     * @ Post: Enemy spawned at location
     */
    void SpawnEnemy(string enemyToSpawn, Vector2 spawnLocation)
    {

        Debug.Log("Spawning " + enemyToSpawn + " at " + spawnLocation);

        Instantiate(Enemy1, spawnLocation, Quaternion.identity);

        //Pick an enemy to spawn randomly
        //Instantiate the enemy at the spawn location passed in
        //Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity);
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
    Stack<string> GenerateWaveMakeup(Wave wave)
    {

        Stack<string> waveMakeup = new Stack<string>();
        
        //Get Percent of Each Enemy Class Per Wave
        float easyEnemiesPercent = PercentageEasyEnemies();
        float mediumEnemiesPercent = PercentageMediumEnemies();
        float hardEnemiesPercent = PercentageHardEnemies();

        //Get Number of Enemies based on Percent
        int numEasyEnemies = Mathf.FloorToInt(easyEnemiesPercent * enemiesPerWave);
        int numMediumEnemies = Mathf.FloorToInt(mediumEnemiesPercent * enemiesPerWave);
        int numHardEnemies = Mathf.FloorToInt(hardEnemiesPercent * enemiesPerWave);

        string[] enemyArray = new string[numEasyEnemies + numHardEnemies + numMediumEnemies];

        int index = 0;
        
        for (int x = 0; x < numEasyEnemies; x++) {
            string enemy = wave.GetEnemy("easy");
            enemyArray[index] = enemy;
            index++;
        }
        for (int x = 0; x < numMediumEnemies; x++)
        {
            string enemy = wave.GetEnemy("medium");
            enemyArray[index] = enemy;
            index++;
        }
        for (int x = 0; x < numHardEnemies; x++)
        {
            string enemy = wave.GetEnemy("hard");
            enemyArray[index] = enemy;
            index++;
        }

        shuffle(enemyArray);
        for (int x = 0; x < enemyArray.Length; x++) {
            waveMakeup.Push(enemyArray[x]);
        }
        return waveMakeup;
    }

    float PercentageEasyEnemies() {
        float easyEnemiesBeforeInfinity;

        int level = levelNumber;

        if (level < roundStartCalculatingEasyPercentage)
        {
            return 1;
        }
        else if (level >= roundEasyEnemiesGoToInfinity) {
            return PercentageEasyEnemiesAtInfinity();
        }

        easyEnemiesBeforeInfinity = ((-0.01458f) * (Mathf.Pow(level,3))) + (0.2687f * (Mathf.Pow(level, 2))) + ((-1.635f) * level) + 3.781f;

        //Debug.Log("Easy Enemies Before Infinity: " + System.Math.Round(easyEnemiesBeforeInfinity,2));

        return easyEnemiesBeforeInfinity;
    }
    float PercentageEasyEnemiesAtInfinity()
    {
        int level = levelNumber;

        float easyEnemiesInfinity = 0.2485f * Mathf.Pow(level, -0.09884f);
        
        //Debug.Log("Easy Enemies At Infinity: " + System.Math.Round(easyEnemiesInfinity, 2));

        return easyEnemiesInfinity;
    }
    float PercentageMediumEnemies()
    {

        int level = levelNumber;

        if (level < roundMediumEnemiesStartSpawning)
        {
            return 0;
        }
        else if (level >= roundMediumEnemiesGoToInfinity)
        {
            return PercentageMediumEnemiesAtInfinity();
        }

        float mediumEnemiesBeforeInfinity = (0.1923f * Mathf.Pow(level,2) + -0.3077f * level + 0.1154f) / (Mathf.Pow(level, 2) + -9.231f * level + 27.92f);

        //Debug.Log("Medium Enemies Before Infinity: " + System.Math.Round(mediumEnemiesBeforeInfinity, 2));

        return mediumEnemiesBeforeInfinity;

    }
    float PercentageMediumEnemiesAtInfinity()
    {

        int level = levelNumber;

        float mediumEnemiesInfinity = 0.5362f * Mathf.Pow(level,-0.03182f);

        //Debug.Log("Medium Enemies At Infinity: " + System.Math.Round(mediumEnemiesInfinity, 2));

        return mediumEnemiesInfinity;
    }
    float PercentageHardEnemies()
    {

        int level = levelNumber;

        if (level < roundHardEnemiesStartSpawning)
        {
            return 0;
        }
        else if (level >= roundHardEnemisGoToInfinity)
        {
            return PercentageHardEnemiesAtInfinity();
        }

        float hardEnemiesBeforeInfinity = 0.00002022f * Mathf.Pow(level, 4.371f);

        //Debug.Log("Hard Enemies Before Infinity: " + System.Math.Round(hardEnemiesBeforeInfinity, 2));

        return hardEnemiesBeforeInfinity;
    }
    float PercentageHardEnemiesAtInfinity(){

        int level = levelNumber;

        float hardEnemiesInfinity = 0.2556f * Mathf.Pow(level, 0.07284f);

        //Debug.Log("Hard Enemies At Infinity: " + System.Math.Round(hardEnemiesInfinity, 2));

        return hardEnemiesInfinity;
    }

    int CalculateTimeBetweenWaves() {

        float time = ((1/(Mathf.Pow(levelNumber, 1.01f) + 3 + Mathf.Cos(levelNumber * (Mathf.PI * 0.5f))) * 35) + 4);

        //Debug.Log("Time Between Waves = " + Mathf.FloorToInt(timeBetweenWaves));

        return Mathf.FloorToInt(time);
    }

    int CalculateEnemiesPerWave() {

        float waveCount = Mathf.Log(levelNumber + 1) * 5;

        //Debug.Log("Enemies per wave = " + Mathf.FloorToInt(waveCount));

        return Mathf.FloorToInt(waveCount);

    }

    /* @ Param: string array that needs to be randomly shuffled
    * @ Pre: None
    * @ Post: Returns a randomly sorted array
    */
    void shuffle(string[] texts)
    {
        for (int t = 0; t < texts.Length; t++)
        {
            
            string tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
    }

    /* @ Param: string array holds array to search, string itemLookingFor holds item to be found
     * @ Pre: None
     * @ Post: Returns true if item is in array
     */ 
    bool IsPresent(string[] array, string itemLookingFor) {

        for (int x = 0; x < array.Length; x++) {
            if (array[x] == itemLookingFor){
                return true;
            }
        }
        return false;
    }
}

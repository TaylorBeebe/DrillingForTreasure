using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    
    //Array holding the different enemy types
    [SerializeField] GameObject enemy1, enemy2, enemy3;

    //get and create set for array so it can be initialized at start
    public GameObject[] enemyarray {
        get;
        private set;
    }

    public enum SpawnState {
        SPAWNING,
        WAITING,
        COUNTING
    };

    [System.Serializable]
    public class Wave {
        public string name;
        public int count;
        public float rate;
        public Transform location;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    public SpawnState state = SpawnState.COUNTING;

    void Start() {
        waveCountdown = timeBetweenWaves;

        //initialize array
        enemyarray = new GameObject[] {
            enemy1, enemy2, enemy3
        };

    }

    void Update() {
        if (state == SpawnState.WAITING) {


        }
        if (waveCountdown <= 0) {
            //If we are already spawning, we don't want to start a new coroutine for spawning
            if (state != SpawnState.SPAWNING)
            {
                //Coroutine spawns waves so they don't need to be spawned all at one time
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
            else {
                waveCountdown -= Time.deltaTime;
            }

        }
    }

    bool AreEnemiesAlive() {
        if (GameObject.FindGameObjectWithTag("Enemy") == null) {
            //enemy with tag found
            return false;
        }
        //return true if no enemy with tag is found
        return true;
    }

    IEnumerator SpawnWave(Wave wave) {

        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++) {
            SpawnEnemy(wave.location);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform spawnLocation) {
        Debug.Log("Spawning Enemies at " + spawnLocation.position);
    }
}

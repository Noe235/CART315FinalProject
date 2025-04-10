using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    
    public static int enemiesAlive = 0;
    
    // These are references to the two prefabs 
    // e.g. [0] = PlayerAttacker, [1] = CoreAttacker
    [SerializeField] public GameObject[] Enemies;
    
    // Current wave
    private int waveIndex = 1;
    private bool waveInProgress = false;    // are we currently spawning?
    private bool autoWaveStarted = false;
    
    [SerializeField] public float SpawnRate;
    [SerializeField] private int minRx,maxRx,minRy,maxRy;
        
    // scene load settings: start from wave 1
    void Awake() {
        waveIndex = 1;
        enemiesAlive = 0;
        waveInProgress = false;
        autoWaveStarted = false;
    }
    
    // Update is called once per frame
    void Update() {
        // If no wave is spawning AND enemiesAlive == 0, press "I" to start Wave 1
        if (!waveInProgress && enemiesAlive <= 0 && waveIndex == 1) {
            if (Input.GetKeyDown(KeyCode.I)) {
                Debug.Log("Wave " + waveIndex + " beginning!");
                StartCoroutine(SpawnWave());
            }
        }
        
        //  wave cleared => Start next wave countdown (waveInProgress = false, enemiesAlive = 0, waveIndex > 1)
        if (!waveInProgress && enemiesAlive <= 0 && waveIndex > 1 && !autoWaveStarted) {
            autoWaveStarted = true;
            StartCoroutine(AutoSpawnNextWave(5f));
        }
    }
    
    // spawns a specific number of enemies
    private IEnumerator SpawnWave() {
        waveInProgress = true;
        // waveIndex 1 => 10 enemies, waveIndex 2 => 20, waveIndex 3 => 30, etc.
        int countThisWave = 10 * waveIndex;
        Debug.Log("Spawning wave #" + waveIndex + " with " + countThisWave + " enemies");

        for (int i = 0; i < countThisWave; i++) {
            SpawnEnemy();
            enemiesAlive++;  // increment count after spawning
            yield return new WaitForSeconds(SpawnRate);
        }

        // wave complete
        waveIndex++; // next wave will have 10 more
        waveInProgress = false;
        autoWaveStarted = false; // next wave auto starts when enemies = 0
    }
    
    //
    private IEnumerator AutoSpawnNextWave(float delay) {
        float remaining = delay;
        while (remaining > 0) {
            Debug.Log($"Wave {waveIndex} will begin in {remaining} second(s)...");
            // Countdown next wave every second in debug log
            yield return new WaitForSeconds(1f);
            remaining -= 1f;
        }
        // wave spawn check
        if (!waveInProgress && enemiesAlive <= 0) {
            Debug.Log("Wave " + waveIndex + " beginning now!");
            StartCoroutine(SpawnWave());
        }
    }
    

    // change to now spawn a single enemy instead of looping here
    private void SpawnEnemy() {
        GameObject spawnee = Enemies[Random.Range(0, Enemies.Length)];
        
        int rX = Random.Range(minRx, maxRx);
        int rY = Random.Range(minRy, maxRy);
        Vector3 loc = new Vector3(rX, 0, rY);
        
        Instantiate(spawnee, loc, Quaternion.identity);
    }
}

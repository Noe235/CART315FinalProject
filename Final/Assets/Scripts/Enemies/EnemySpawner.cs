using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    
    public static int enemiesAlive = 0;
    
    // These are references to the two prefabs 
    // e.g. [0] = PlayerAttacker, [1] = CoreAttacker
    [SerializeField] public GameObject[] Enemies;
    
    // Current wave
    private int waveIndex = 1;
    
    // are we currently spawning?
    private bool waveInProgress = false;
    
    [SerializeField] public float SpawnRate;
    [SerializeField] private int minRx,maxRx,minRy,maxRy;

    // Update is called once per frame
    void Update() {
        // If no wave is spawning AND enemiesAlive == 0, press "I" to start next wave
        if (!waveInProgress && enemiesAlive <= 0) {
            if (Input.GetKeyDown(KeyCode.I)) {
                Debug.Log("Wave " + waveIndex + " beginning!");
                StartCoroutine(SpawnWave());
            }
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

        waveIndex++; // next wave will have 10 more
        waveInProgress = false;
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

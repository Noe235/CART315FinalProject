using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    
    public static int enemiesAlive = 0;
    public static int currentRunScore = 0;
    
    
    // These are references to the two prefabs 
    // e.g. [0] = PlayerAttacker, [1] = CoreAttacker
    [SerializeField] public GameObject[] Enemies;
    
    // Current wave
    public static int waveIndex = 1;
    private bool waveInProgress = false;    // are we currently spawning?
    private bool autoWaveStarted = false;
    private bool firstWaveCountdownStarted = false;
    
    [SerializeField] public float SpawnRate;
    [SerializeField] private int minRx,maxRx,minRy,maxRy;
        
    // scene load settings: start from wave 1
    void Awake() {
        enemiesAlive = 0;
        // waveIndex = 1;
        // currentRunScore = 0;
        // waveInProgress = false;
        // autoWaveStarted = false;
    }
    
    // Update is called once per frame
    void Update() {
        // start wave 1 automatically after initial countdown
        if (!waveInProgress && enemiesAlive <= 0 && waveIndex == 1 && !firstWaveCountdownStarted)
        {
            firstWaveCountdownStarted = true;
            StartCoroutine(FirstWaveCountdown());
        }
        
        //  wave cleared => Start next wave countdown (waveInProgress = false, enemiesAlive = 0, waveIndex > 1)
        if (!waveInProgress && enemiesAlive <= 0 && waveIndex > 1 && !autoWaveStarted) {
            autoWaveStarted = true;
            StartCoroutine(AutoSpawnNextWave(10f));
        }
    }
    
    private IEnumerator FirstWaveCountdown()
    {
        int remaining = 5;                     // show 5‑4‑3‑2‑1
        while (remaining > 0)
        {
            GameMessageUI.Instance.Show($"First wave spawning in {remaining} second(s)", 1f);
            yield return new WaitForSeconds(1f);
            remaining--;
        }

        var msg = $"Wave {waveIndex} beginning!";
        GameMessageUI.Instance.Show(msg, 3f);
        Debug.Log(msg);

        StartCoroutine(SpawnWave());           // kick off Wave
    }
    
    // spawns a specific number of enemies
    private IEnumerator SpawnWave() {
        waveInProgress = true;
        // waveIndex 1 => 5 enemies, waveIndex 2 => 10, waveIndex 3 => 15, etc.
        int countThisWave = 5 * waveIndex;
        var msg = "Spawning wave #" + waveIndex + " with " + countThisWave + " enemies";
        GameMessageUI.Instance.Show(msg, 2f);
        Debug.Log(msg);

        for (int i = 0; i < countThisWave; i++) {
            SpawnEnemy();
            enemiesAlive++;  // increment count after spawning
            yield return new WaitForSeconds(SpawnRate);
        }

        // wave complete
        waveIndex++; // next wave will have 5 more
        waveInProgress = false;
        autoWaveStarted = false; // next wave auto starts when enemies = 0
    }
    
    //
    private IEnumerator AutoSpawnNextWave(float delay) {
        float remaining = delay;
        while (remaining > 0) {
            var msg = $"Wave {waveIndex} will begin in {remaining} second(s)...";
            GameMessageUI.Instance.Show(msg, 1f);
            Debug.Log(msg);
            // Countdown next wave every second in debug log
            yield return new WaitForSeconds(1f);
            remaining -= 1f;
        }
        // wave spawn check
        if (!waveInProgress && enemiesAlive <= 0) {
            var msg = "Wave " + waveIndex + " beginning now!";
            GameMessageUI.Instance.Show(msg, 3f);
            Debug.Log(msg);
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
    
    public static void EnemyDied()
    {
        enemiesAlive--;
        currentRunScore += 1; // add 1 point per kill
        Debug.Log("Killed enemy, " + enemiesAlive + " remaining");
    }
    
    
}

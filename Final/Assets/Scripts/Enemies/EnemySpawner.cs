using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
    public GameObject[] Enemies;
    public float SpawnRate;
    public float SpawnPositions;

    [SerializeField] private int minRx,maxRx,minRy,maxRy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        StartCoroutine(enemiesSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator enemiesSpawner() {
        int rX = Random.Range(minRx, maxRx);
        int rY = Random.Range(minRy, maxRy);
        GameObject spawnee = Enemies[Random.Range(0, Enemies.Length)];
        
        Vector3 Loc = new Vector3(rX, 0, rY);
        Instantiate(spawnee, Loc, Quaternion.identity);

        float next = SpawnRate;
        yield return new WaitForSeconds(next);
        StartCoroutine(enemiesSpawner());

    }
}

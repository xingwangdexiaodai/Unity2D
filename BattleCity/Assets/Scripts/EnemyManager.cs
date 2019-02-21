using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField]
    private int totalGeneratedEnemyCount = 8;
    private int curGeneratedEnemyCount;

    [SerializeField]
    private int totalDisplayedEnemyCount = 3;
    private int curDisplayedEnemyCount;

    public GameObject[] spawnPositions;

    public GameObject[] enemyPFs;

    private void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnOnStart", 2f);
    }

    public void SpawnOnDestroy() {
        --curDisplayedEnemyCount;

        if (curGeneratedEnemyCount >= totalGeneratedEnemyCount) return;

        if (curDisplayedEnemyCount >= totalDisplayedEnemyCount) return;

        int enemyIndex = Random.Range(0, enemyPFs.Length);
        int posIndex = Random.Range(0, spawnPositions.Length);
        Spawn(enemyIndex, posIndex);
    }

    void SpawnOnStart() { 
        for(int i = 0; i < spawnPositions.Length; ++i) {
            int enemyIndex = Random.Range(0, enemyPFs.Length);
            Spawn(enemyIndex, i);
        }
    }

    void Spawn(int enemyIdex, int posIndex) {
        ++curDisplayedEnemyCount;
        ++curGeneratedEnemyCount;
        Instantiate(
            enemyPFs[enemyIdex],
            spawnPositions[posIndex].transform.position,
            Quaternion.identity);
    }
}

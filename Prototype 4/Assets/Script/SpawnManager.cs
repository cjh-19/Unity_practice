using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    // 스폰하고자 하는 객체를 저장한 배열
    public GameObject[] enemyPrefab;
    // powerup 프리팹 변수
    public GameObject[] powerupPrefab;
    // 랜덤한 위치에서 생성
    private float spawnRange = 9;

    // 적이 몇개인지 카운트
    public int enemyCount;
    public int waveNumber = 1; // 적이 매번 생기는 개수

    // Start is called before the first frame update
    void Start()
    {
        int randomPowerup = Random.Range(0, powerupPrefab.Length);
        Instantiate(powerupPrefab[randomPowerup], GenerateSpawnPosition(), powerupPrefab[randomPowerup].transform.rotation);

        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length; // object"s". s있음 조심
        // 모든 적을 제거했다면
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            int randomPowerup = Random.Range(0, powerupPrefab.Length);
            Instantiate(powerupPrefab[randomPowerup], GenerateSpawnPosition(), powerupPrefab[randomPowerup].transform.rotation);
        }
    }
    
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // 여러개의 적을 생성
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemy], GenerateSpawnPosition(), enemyPrefab[randomEnemy].transform.rotation);
        }

    }

    // 임의의 스폰 포지션을 생성해주는 함수
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}